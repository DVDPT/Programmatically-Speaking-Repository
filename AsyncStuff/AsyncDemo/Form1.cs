using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncDemo
{
    public partial class Form1 : Form
    {


        #region Initialization

        private CpuBoundOperationContainer _operationsContainer = null;
        private SynchronizationContext _guiSynchronizationContext;
        public Form1()
        {
            InitializeComponent();
            _stopBut.Enabled = false;
            _operationsContainer = new CpuBoundOperationContainer
                (
                    new CpuBoundOperationUi(_op0ProgressBar, _op0Label, _op0StopButton, MIN_OPERATION_TIME),
                    new CpuBoundOperationUi(_op1ProgressBar, _op1Label, _op1StopButton, MIN_OPERATION_TIME + TIME_INCREMENT * 2),
                    new CpuBoundOperationUi(_op2ProgressBar, _op2Label, _op2StopButton, MIN_OPERATION_TIME + TIME_INCREMENT * 3),
                    new CpuBoundOperationUi(_op3ProgressBar, _op3Label, _op3StopButton, MIN_OPERATION_TIME + TIME_INCREMENT * 4)
                );
            _guiSynchronizationContext = SynchronizationContext.Current;

        }
        #endregion

        #region AuxClasses

        internal class CpuBoundOperationUi
        {
            private readonly ProgressBar _progressBar;
            private readonly Label _label;
            private readonly Button _stopButton;
            public readonly int OperationTime;
            private volatile bool _wasCancelled = false;

            public bool WasCancelled { get { return _wasCancelled; } }

            public CpuBoundOperationUi(ProgressBar progressBar, Label label, Button stopButton, int operationTime)
            {
                _progressBar = progressBar;
                OperationTime = operationTime;
                _label = label;
                _stopButton = stopButton;
                Reset();
                _stopButton.Click += (obj, args) =>
                                         {
                                             Cancel();
                                         };

            }

            public void Cancel() { _wasCancelled = true; }

            public void Reset()
            {
                _label.Text = "Stopped";
                _stopButton.Enabled = false;
                _progressBar.Value = 0;
                _wasCancelled = false;
            }

            public void OnStarted()
            {
                Reset();

                _label.Text = "Running";
                _stopButton.Enabled = true;
            }

            public void OnEnded()
            {
                _stopButton.Enabled = false;
                _label.Text = "Finished";
            }

            public void OnUpdate(int val)
            {
                _progressBar.Value = val;
            }

            public void OnCancel()
            {
                _label.Text = "Cancelled";
                _stopButton.Enabled = false;
            }
        }

        internal class CpuBoundOperationContainer
        {
            public readonly CpuBoundOperationUi[] Operations;

            public CpuBoundOperationContainer(params CpuBoundOperationUi[] operations)
            {
                Operations = operations;
            }
        }



        private void ControlStartStopButtons(bool started)
        {
            _stopBut.Enabled = started;
            _startBut.Enabled = !started;
        }
        #endregion

        #region Synchronous
        /*/

        private const int MIN_OPERATION_TIME = 1000;
        private const int TIME_INCREMENT = 500;
        private const int UPDATE_FREQUENCY = 10000;

        private void _startBut_Click(object sender, EventArgs e)
        {
            ControlStartStopButtons(true);

            foreach (CpuBoundOperationUi operation in _operationsContainer.Operations)
            {
                LaunchCpuBoundOperation(operation);
            }

            ControlStartStopButtons(false);
        }

        private void LaunchCpuBoundOperation(CpuBoundOperationUi op)
        {
            op.OnStarted();

            var wasCompleted = CpuBoundOperation(op);

            if (wasCompleted)
               op.OnEnded();
            else
                op.Cancel();
        }

        private bool CpuBoundOperation(CpuBoundOperationUi op)
        {
            var timeToExit = Environment.TickCount + op.OperationTime;
            var i = 0;
            int currentTicks;
           
            while ((currentTicks = Environment.TickCount) < timeToExit)
            {
                if (i++ % UPDATE_FREQUENCY == 0)
                {
                    DoUpdateOperationStatus(op, 100 * currentTicks / timeToExit);
                }

                if (op.WasCancelled)
                    return false;

                
            }
            return true;
        }

        private void DoUpdateOperationStatus(CpuBoundOperationUi op, int value)
        {
            op.OnUpdate(value);
        }


        private void _stopBut_Click(object sender, EventArgs e)
        {
            foreach (CpuBoundOperationUi operation in _operationsContainer.Operations)
            {

                operation.Cancel();

            }
        }

        //*/
        #endregion

        #region APM
        /*/
        private const int MIN_OPERATION_TIME = 2000;
        private const int TIME_INCREMENT = 1000;
        private const int UPDATE_FREQUENCY = 10000;




        private void _startBut_Click(object sender, EventArgs e)
        {
            ControlStartStopButtons(true);

            Action onCompleted = () => ControlStartStopButtons(false);


            LaunchCpuBoundFunctionsAsync(onCompleted);



        }

        private void LaunchCpuBoundFunctionsAsync(Action onCompleted)
        {

            var guiContext = SynchronizationContext.Current;
            int operationsFinished = 0;

            AsyncCallback callback = (ar) =>
                                         {
                                             var func = ar.AsyncState as Action;
                                             func.EndInvoke(ar);

                                             if (Interlocked.Increment(ref operationsFinished) == _operationsContainer.Operations.Length)
                                             {
                                                 guiContext.Post(_=>onCompleted(),null);
                                             }
                                         };

            foreach (CpuBoundOperationUi operation in _operationsContainer.Operations)
            {
                var op = operation;
                Action function = () =>
                                      {

                                          guiContext.Post(_ => op.OnStarted(), null);

                                          if (LaunchCpuBoundOperation(op, guiContext)) 
                                              guiContext.Post(_ => op.OnEnded(), null);
                                          else
                                              guiContext.Post(_=>op.OnCancel(),null);
                                      };



                function.BeginInvoke(callback, function);


            }
        }

        private bool LaunchCpuBoundOperation(CpuBoundOperationUi op, SynchronizationContext guiContext)
        {
            var timeToExit = Environment.TickCount + op.OperationTime;
            var i = 0;
            int currentTicks;

            while ((currentTicks = Environment.TickCount) < timeToExit)
            {
                if (i % UPDATE_FREQUENCY == 0)
                {
                    DoUpdateOperationStatus(op, 100 - (100 * (timeToExit - currentTicks)) / op.OperationTime, guiContext);
                    if (op.WasCancelled)
                    {
                        return false;
                    }
                }
                ++i;
            }
            return true;
        }

        private void DoUpdateOperationStatus(CpuBoundOperationUi op, int value, SynchronizationContext guiContext)
        {
            guiContext.Send(_ => op.OnUpdate(value), null);
        }

        private void _stopBut_Click(object sender, EventArgs e)
        {
            foreach (CpuBoundOperationUi operation in _operationsContainer.Operations)
            {

                operation.Cancel();
                
            }
        }

        //*/
        #endregion

        #region Async
        //
        private const int MIN_OPERATION_TIME = 2000;
        private const int TIME_INCREMENT = 1000;
        private const int UPDATE_FREQUENCY = 10000;




        private async void _startBut_Click(object sender, EventArgs e)
        {

            ControlStartStopButtons(true);
            var operations = _operationsContainer.Operations;

           

          
            var tasks = new Task[operations.Length];

            //for (int i = 0; i < operations.Length; ++i)
            //{
            //    tasks[i] = LaunchCpuBoundOperationAsync(operations[i]);
            //}

            //await TaskEx.WhenAll(tasks);
           
         

            await LaunchCpuBoundOperationAsync(operations[0]);


            ControlStartStopButtons(false);
        }

        private async Task LaunchCpuBoundOperationAsync
            (CpuBoundOperationUi op)
        {
           

            op.OnStarted();

            bool wasCompleted = await CpuBoundOperation(op);
            
            if(wasCompleted)
                op.OnEnded();
            
            else
               op.OnCancel();
        }


        private Task<bool> CpuBoundOperation(CpuBoundOperationUi op)
        {
            return TaskEx.Run(() =>
            {
                var timeToExit = Environment.TickCount + op.OperationTime;
                var i = 0;
                int currentTicks;
                while ((currentTicks = Environment.TickCount) < timeToExit)
                {
                    if (op.WasCancelled)
                    {
                        return false;
                    }

                    if (i % UPDATE_FREQUENCY == 0)
                    {
                        DoUpdateOperationStatusAsync(op, 100 - (100 * (timeToExit - currentTicks)) / op.OperationTime);
                    }


                    ++i;
                }
                return true;
            });
        }


        private async void DoUpdateOperationStatusAsync(CpuBoundOperationUi op, int value)
        {
            await _guiSynchronizationContext.SwitchTo();
            op.OnUpdate(value);
        }

        private void _stopBut_Click(object sender, EventArgs e)
        {
            foreach (CpuBoundOperationUi operation in _operationsContainer.Operations)
            {

                operation.Cancel();

            }
        }


        //*/
        #endregion
    }


    public static class Extensions
    {
        public static IEnumerable<int> To(this int from, int to)
        {
            while (from < to)
                yield return from++;
        }

    public static SynchronizationContextAwaiter SwitchTo(this SynchronizationContext context) { return new SynchronizationContextAwaiter { m_context = context }; }

        public struct SynchronizationContextAwaiter
        {
            internal SynchronizationContext m_context;
            public SynchronizationContextAwaiter GetAwaiter()
            {
                return this;
            }
            public bool IsCompleted { get { return false; } }
            public void GetResult() { }
            public void OnCompleted(Action continuation) { m_context.Post(s => ((Action)s)(), continuation); }
        }
    }

}

