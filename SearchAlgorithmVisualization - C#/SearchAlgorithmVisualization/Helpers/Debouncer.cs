using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmVisualization.Helpers
{
    public class Debouncer
    {
        private System.Threading.Timer? timer;

        public T? Debounce<T>(Func<T> action, int delayMilliseconds)
        {
            // Initialize default result
            T? result = default(T);

            // If there's an existing timer, dispose it to prevent multiple concurrent timers
            this.timer?.Dispose();

            this.timer = new System.Threading.Timer(_ =>
            {
                result = action(); // Exectue the action after the delay
            }, null, delayMilliseconds, Timeout.Infinite);

            // Wait for the timer to complete
            Thread.Sleep(delayMilliseconds + 20); // Ensure the timer finishes before returning

            return result;
        }

        public void Debounce(Action action, int delayMilliseconds, SynchronizationContext? syncContext = null)
        {
            // If there's an existing timer, dispose it to prevent multiple concurrent timers
            timer?.Dispose();

            timer = new System.Threading.Timer(_ =>
            {
                if (syncContext != null)
                {
                    // Execute the action after the delay on the main UI thread
                    syncContext.Post(__ => action(), null);
                } else
                {
                    action(); // Execute the action after the delay
                }
            }, null, delayMilliseconds, Timeout.Infinite);
        }
    }
}
