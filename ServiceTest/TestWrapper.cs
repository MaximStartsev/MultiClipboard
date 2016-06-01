namespace ServiceTest
{
    class TestWrapper: MultiClipboard.MultiClipboardService
    {
        public void TestStart(string[] args)
        {
            OnStart(args);
        }

        public void TestStop()
        {
            OnStop();
        }
    }
}
