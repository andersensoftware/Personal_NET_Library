using System;
using System.Threading;

namespace NetExtentions
{
    public static class ActionExtentions
    {
        /// <summary>
        /// Polls aPolledCondition until it returns true or until timeout occurs.
        /// 
        /// If the condition is reached before the timeout then the OnSuccessAction is called.
        /// If a timeout occurs then OnTimeout is called
        /// </summary>
        /// <param name="aPolledCondition">The condition to poll, function should return true when the condition is reached, or false otherwise</param>
        /// <param name="aOnTimeoutAction">An action to perform should the poll times out, Return true [default] if an exception is to be thrown, false otherwise signals the condition was handled and no exceptions will be thrown</param>
        /// <param name="aOnSuccessAction">An Action to perform should the poll succeed before the timeout period</param>
        /// <param name="aTimeout">An Time to wait before timing out</param>
        /// <param name="aHeartbeatInterval">An interval at which polling should occur</param>
        /// <param name="aTimeoutMessage">Timout message</param>
        /// <param name="aActionName">Name for the action to put in the logs</param>
        /// <param name="aCancelPollingEvent">a ManualResetEvent to signal should we want to cancel the polling loop</param>
        public static void PollWithTimeout(this Func<bool> aPolledCondition,
            Func<bool> aOnTimeoutAction = null,
            Action aOnSuccessAction = null,
            TimeSpan? aTimeout = null,
            TimeSpan? aHeartbeatInterval = null,
            string aTimeoutMessage = null,
            string aActionName = null,
            ManualResetEvent aCancelPollingEvent = null)
        {
            if (aTimeoutMessage == null) aTimeoutMessage = "Timeout occured while waiting for action to complete";
            if (aPolledCondition == null) throw new ArgumentNullException("aPolledCondition");

            bool waitInfinitely = !aTimeout.HasValue;
            bool conditionReached = false;
            TimeSpan heartbeatTimeSpan = aHeartbeatInterval.HasValue ? aHeartbeatInterval.Value : TimeSpan.FromSeconds(5);
            if (!waitInfinitely)
            {
                heartbeatTimeSpan = aTimeout > heartbeatTimeSpan ? heartbeatTimeSpan : TimeSpan.FromMilliseconds(aTimeout.Value.Milliseconds / 10.0);
                //aPolledCondition.Log().Info("heartbeat for polling set to {0}", heartbeatTimeSpan);
            }
            DateTime actionStartedDateTime = DateTime.Now;
            DateTime lastMessageHeartbeat = DateTime.Now;
            TimeoutException ex = null;
            IAsyncResult result = null;
            bool finished = false;
            bool cancelled = false;
            do
            {
                if (result == null || (!conditionReached && result.IsCompleted))
                {
                    result = aPolledCondition.BeginInvoke(ar =>
                    {
                        conditionReached = aPolledCondition.EndInvoke(ar);
                    }, null);
                }

                //We give up to the Heartbeat for the polling condition to be evaluated
                result.AsyncWaitHandle.WaitOne(heartbeatTimeSpan);

                if (result.IsCompleted && conditionReached)
                {
                    finished = true;
                }
                else
                {
                    DateTime nowDateTime = DateTime.Now;
                    if (!waitInfinitely && (nowDateTime - actionStartedDateTime >= aTimeout))
                    {
                        finished = true;
                        ex = new TimeoutException(String.Format("{0}, total time waited [{1}]",
                                aTimeoutMessage, nowDateTime - actionStartedDateTime));
                    }
                    else
                    {
                        if (DateTime.Now - lastMessageHeartbeat >= heartbeatTimeSpan)
                        {
                            lastMessageHeartbeat = DateTime.Now;
                        }
                        if (result.IsCompleted)
                        {
                            //this wait is needed sicne the waitOne above will typically take very short time to complete
                            //it is there only to prevent blockage in the evaluation of the condition
                            //Here we wait 5 seconds before we evaluate the condition again
                            //or the heartbeat timeout whichever one is fastest
                            if (heartbeatTimeSpan > TimeSpan.FromSeconds(5))
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(5));
                            }
                            else
                            {
                                Thread.Sleep(heartbeatTimeSpan);
                            }
                        }
                    }
                }

                if (aCancelPollingEvent != null)
                {
                    cancelled = aCancelPollingEvent.WaitOne(0);
                }
            } while (!finished && !cancelled);

            if (!cancelled)
            {
                //Ok.. we got out of the loop...   
                if (conditionReached)
                {
                    if (aOnSuccessAction != null)
                    {
                        aOnSuccessAction.Invoke();
                    }
                }
                else
                {
                    bool throwException = true;
                    if (aOnTimeoutAction != null)
                    {
                        throwException = aOnTimeoutAction.Invoke();
                    }

                    if (throwException && ex != null)
                    {
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// Allows to stop action execution after a set timespan has elapsed.
        /// 
        /// When timeout is reached a TimeoutException will be thrown
        /// </summary>
        /// <param name="aSourceAction">The action to execute</param>
        /// <param name="aTimeout">Timespan after which the action will fail</param>
        /// <param name="aHeartbeatInterval">Timespan after which the Invoke will check if timeout has been reached</param>
        public static void InvokeWithTimeout(this Action<ManualResetEvent> aSourceAction,
            TimeSpan? aTimeout = null,
            TimeSpan? aHeartbeatInterval = null)
        {
            if (aSourceAction == null) throw new ArgumentNullException("aSourceAction");

            ManualResetEvent timeoutEvent = new ManualResetEvent(false);
            bool waitInfinitely = !aTimeout.HasValue;
            bool waiting = true;
            timeoutEvent.Reset();

            var result = aSourceAction.BeginInvoke(timeoutEvent, ar =>
            {
                waiting = false;
                aSourceAction.EndInvoke(ar);
            }, null);

            TimeSpan heartbeatTimeSpan = aHeartbeatInterval.HasValue ? aHeartbeatInterval.Value : TimeSpan.FromSeconds(5);
            if (aTimeout.HasValue)
            {
                if (heartbeatTimeSpan >= aTimeout)
                {
                    heartbeatTimeSpan = TimeSpan.FromMilliseconds(aTimeout.Value.Milliseconds/10.0);
                }
            }
            if (!waitInfinitely)
            {
                heartbeatTimeSpan = aTimeout > heartbeatTimeSpan ? heartbeatTimeSpan : aTimeout.Value;
            }
            DateTime taskStartedDateTime = DateTime.Now;

            while (waitInfinitely || waiting)
            {
                result.AsyncWaitHandle.WaitOne(heartbeatTimeSpan);
                if (!result.IsCompleted)
                {
                    if (!waitInfinitely && (DateTime.Now - taskStartedDateTime >= aTimeout))
                    {
                        waiting = false;
                        timeoutEvent.Set();
                        if (result.AsyncWaitHandle.WaitOne(heartbeatTimeSpan.Milliseconds * 2))
                        {
                            throw new TimeoutException(String.Format("Timeout occured while waiting for action to complete, Action quit gracefully, total time waited [{0}]", DateTime.Now - taskStartedDateTime));
                        }
                        throw new TimeoutException(String.Format("Timeout occured while waiting for action to complete, Action did not respond to shutdown signal within [{1}], total time waited [{0}]", DateTime.Now - taskStartedDateTime, TimeSpan.FromMilliseconds(heartbeatTimeSpan.Milliseconds * 2)));
                    }
                }
            }
        }
    }
}
