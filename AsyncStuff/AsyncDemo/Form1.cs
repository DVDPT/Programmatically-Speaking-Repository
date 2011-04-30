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

        public Form1()
        {
            InitializeComponent();
            _stopBut.Enabled = false;
            _operationsContainer = new CpuBoundOperationContainer
                (
                    new CpuBoundOperation(_op0ProgressBar, _op0Label, _op0StopButton, MIN_OPERATION_TIME),
                    new CpuBoundOperation(_op1ProgressBar, _op1Label, _op1StopButton, MIN_OPERATION_TIME + TIME_INCREMENT),
                    new CpuBoundOperation(_op2ProgressBar, _op2Label, _op2StopButton, MIN_OPERATION_TIME + TIME_INCREMENT),
                    new CpuBoundOperation(_op3ProgressBar, _op3Label, _op3StopButton, MIN_OPERATION_TIME + TIME_INCREMENT)
                );

        }
        #endregion

        #region AuxClasses

        internal class CpuBoundOperation
        {
            private readonly ProgressBar _progressBar;
            private readonly Label _label;
            private readonly Button _stopButton;
            public readonly int OperationTime;
            private volatile bool _wasCancelled = false;

            public bool WasCancelled { get { return _wasCancelled; } }

            public CpuBoundOperation(ProgressBar progressBar, Label label, Button stopButton, int operationTime)
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
            public readonly CpuBoundOperation[] Operations;

            public CpuBoundOperationContainer(params CpuBoundOperation[] operations)
            {
                Operations = operations;
            }
        }



        private void ControlStartStopButton(bool started)
        {
            _stopBut.Enabled = started;
            _startBut.Enabled = !started;
        }
        #endregion

        #region Synchronous
        /*/

        private const int MIN_OPERATION_TIME = 1000;
        private const int TIME_INCREMENT = 500;

        private void _startBut_Click(object sender, EventArgs e)
        {
            ControlStartStopButton(true);

            foreach (CpuBoundOperation operation in _operationsContainer.Operations)
            {
                DoLaunchCPUBoundFunction(operation);
            }

            ControlStartStopButton(false);
        }

        private void DoLaunchCPUBoundFunction(CpuBoundOperation op)
        {
            var timeToExit = Environment.TickCount + op.OperationTime;
            var i = 0;
            int currentTicks;
            op.OnStarted();
            while ((currentTicks = Environment.TickCount) < timeToExit)
            {
                if (i % 10 == 0)
                {
                    DoUpdateOperationStatus(op, 100 * currentTicks / timeToExit);
                }
                ++i;
            }
            op.OnEnded();
        }

        private void DoUpdateOperationStatus(CpuBoundOperation op, int value)
        {
            op.OnUpdate(value);
        }


        private void _stopBut_Click(object sender, EventArgs e)
        {
            foreach (CpuBoundOperation operation in _operationsContainer.Operations)
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
            ControlStartStopButton(true);

            Action onCompleted = () => ControlStartStopButton(false);


            LaunchCPUBoundFunctionsAsync(onCompleted);



        }

        private void LaunchCPUBoundFunctionsAsync(Action onCompleted)
        {

            var guiContext = SynchronizationContext.Current;
            int operationsFinished = 0;

            AsyncCallback callback = (ar) =>
                                         {
                                             var func = ar.AsyncState as Action;
                                             func.EndInvoke(ar);

                                             if (Interlocked.Increment(ref operationsFinished) == _operationsContainer.Operations.Count())
                                             {
                                                 guiContext.Post(_=>onCompleted(),null);
                                             }
                                         };

            foreach (CpuBoundOperation operation in _operationsContainer.Operations)
            {
                var op = operation;
                Action function = () =>
                                      {

                                          guiContext.Post(_ => op.OnStarted(), null);

                                          if (DoLaunchCPUBoundFunction(op, guiContext)) 
                                              guiContext.Post(_ => op.OnEnded(), null);
                                          else
                                              guiContext.Post(_=>op.OnCancel(),null);
                                      };



                function.BeginInvoke(callback, function);


            }
        }

        private bool DoLaunchCPUBoundFunction(CpuBoundOperation op, SynchronizationContext guiContext)
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

        private void DoUpdateOperationStatus(CpuBoundOperation op, int value, SynchronizationContext guiContext)
        {
            guiContext.Send(_ => op.OnUpdate(value), null);
        }

        private void _stopBut_Click(object sender, EventArgs e)
        {
            foreach (CpuBoundOperation operation in _operationsContainer.Operations)
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


        private void _startBut_Click(object sender, EventArgs e)
        {
            ControlStartStopButton(true);
            var operations = _operationsContainer.Operations;
            IEnumerable<Task> tasks = 0.To(operations.Length).Select(idx => DoLaunchCPUBoundFunction(operations[idx]));

            ControlStartStopButton(false);
        }

        private async Task DoLaunchCPUBoundFunction(CpuBoundOperation op)
        {
            var timeToExit = Environment.TickCount + op.OperationTime;
            var i = 0;
            await new SynchronizationContext().SwitchTo();
            int currentTicks;
            op.OnStarted();
            while ((currentTicks = Environment.TickCount) < timeToExit)
            {
                if (i % 10 == 0)
                {
                    DoUpdateOperationStatus(op, 100 * currentTicks / timeToExit);
                }
                ++i;
            }
            op.OnEnded();
        }

        private void DoUpdateOperationStatus(CpuBoundOperation op, int value)
        {
            op.OnUpdate(value);
        }

        private void _stopBut_Click(object sender, EventArgs e)
        {
            foreach (CpuBoundOperation operation in _operationsContainer.Operations)
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
    }

}

