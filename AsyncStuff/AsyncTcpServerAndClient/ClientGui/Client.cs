using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGui
{
    public partial class Client : Form
    {
        public class FileRepresentative
        {
            private readonly GroupBox _box;
            private readonly ProgressBar _bar;
            public readonly Button _cancel;
            public readonly Button _remove;
            public readonly Label _state;
            
            public FileRepresentative(GroupBox box, ProgressBar bar, Button cancel, Button remove, Label state)
            {
                _box = box;
                _bar = bar;
                _cancel = cancel;
                _remove = remove;
                _state = state;
                SetVisible(false);
            }
            public void SetFileName(string name) { _box.Text = name;_state.Text = "Running";}
            public void UpdateBar(int to){_bar.Value = to;}
            public void SetVisible(bool visible) { _box.Visible = visible; _remove.Enabled = false; }
            public void EnableRemoveButton() { _remove.Enabled = true; _state.Text = "Completed";
            }
            public void DisableCancelButton(){_cancel.Enabled = false;}
        }

        internal class FileRepresentativeContainer
        {
            private readonly FileRepresentative[] _fileObjects;
            private readonly bool[] _currentBeingUsed;
            public FileRepresentativeContainer(params FileRepresentative[] fileObjects)
            {
                _fileObjects = fileObjects;
                _currentBeingUsed = new bool[_fileObjects.Length];
                
            }

            public FileRepresentative GetFileRepresentative()
            {
                var idx = -1;
                for (int i = 0; i < _currentBeingUsed.Length; ++i)
                {
                    if (!_currentBeingUsed[i])
                    {
                        _currentBeingUsed[i] = true;
                        idx = i;
                        break;
                    }
                }
                return idx == -1 ? null : _fileObjects[idx];
            }

            public void FreeFileDownloading(FileRepresentative obj)
            {
                for (int i = 0; i < _fileObjects.Length; ++i)
                {
                    if (obj == _fileObjects[i])
                    {
                        _currentBeingUsed[i] = false;
                        break;
                    }
                }
                obj.UpdateBar(0);
            }

        }

        private readonly TaskScheduler _scheduler;

        private readonly FileRepresentativeContainer _container;

        private const int SERVER_PORT = 10000;

        public Client()
        {
            InitializeComponent();
            _container = new FileRepresentativeContainer(new FileRepresentative(__FileDownloadingGroup0,__FileDownloadingProgressBar0,__FileDownloadingCancelButton0, __FileDownloadingRemoveButton0, __FileDownloadingStateLabel0),
                                                      new FileRepresentative(__FileDownloadingGroup1, __FileDownloadingProgressBar1, __FileDownloadingCancelButton1, __FileDownloadingRemoveButton1, __FileDownloadingStateLabel1),
                                                      new FileRepresentative(__FileDownloadingGroup2, __FileDownloadingProgressBar2, __FileDownloadingCancelButton2, __FileDownloadingRemoveButton2, __FileDownloadingStateLabel2),
                                                      new FileRepresentative(__FileDownloadingGroup3, __FileDownloadingProgressBar3, __FileDownloadingCancelButton3, __FileDownloadingRemoveButton3, __FileDownloadingStateLabel3));

            _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            __FileNameTextBox.PreviewKeyDown += (s, e) =>
                                                    {
                                                        if (e.KeyData == Keys.Enter)
                                                        {
                                                            __ServerGetFileButton_Click(null,null);
                                                        }
                                                    };

        }

        private void  __ServerGetFileButton_Click(object sender, EventArgs e)
        {
            string fileName;
            if (String.IsNullOrEmpty(fileName = __FileNameTextBox.Text))
                return;
            GetRequestFromServer(new GetFileRequest(fileName), fileName);
        }

        private async void __ServerGetHttpPageButton_Click(object sender, EventArgs e)
        {
            string url;
            if (String.IsNullOrEmpty(url = __FileNameTextBox.Text))
                return;


            var urlFileName = (url + ".html").Replace("/", "").Replace(":", "");
            
            
            var realPath = Utils.RelativePath + urlFileName;


            if (await GetRequestFromServer(new GetHttpRequest(url), urlFileName))
            {
                var browser = new ProcessStartInfo(realPath);
                browser.CreateNoWindow = true;
                Process.Start(browser);
            }
        }



        private async Task<bool> GetRequestFromServer(IRequest request, string fileName)
        {

            var fileRepresentative = _container.GetFileRepresentative();
            
            if (fileRepresentative == null)
                return false;

            fileRepresentative.SetFileName(fileName);
            fileRepresentative.SetVisible(true);
            
            var client = await GetClient();
            var stream = client.GetStream();

            bool succeded;
            try
            {
                succeded = await Utils.GetFile(request,fileName, stream, fileRepresentative, _scheduler);
            }
            catch (Exception)
            {
                succeded = false;
            }
            try
            {
                stream.Close();
                (client as IDisposable).Dispose();
            }
            catch (ObjectDisposedException)
            {
                
            }
            await _scheduler.SwitchTo();

            if (!succeded)
            {
                fileRepresentative.SetVisible(false);
                _container.FreeFileDownloading(fileRepresentative);
                return false;
            }


            EventHandler removeClick = null;

            removeClick = async (_, __) =>
            {
                await _scheduler.SwitchTo();
                fileRepresentative._remove.Click -= removeClick;
                fileRepresentative.SetVisible(false);
                _container.FreeFileDownloading(fileRepresentative);
            };

            fileRepresentative._remove.Click += removeClick;
            fileRepresentative.DisableCancelButton();
            fileRepresentative.EnableRemoveButton();
            return true;

        }

        public static async Task<TcpClient> GetClient()
        {
            
            return await TaskEx.Run(()=>new TcpClient("localhost",SERVER_PORT));
        }

        



    }
}
