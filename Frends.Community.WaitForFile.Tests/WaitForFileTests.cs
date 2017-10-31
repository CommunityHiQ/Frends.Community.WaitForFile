using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Frends.Community.WaitForFile.Tests
{
    [TestFixture]
    public class WaitForFileToAppearTests
    {
        private readonly string _dir = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\TestData");

        [SetUp]
        public void Setup()
        {
        }

       [TearDown]
        public void Down()
        {
            if (File.Exists(Path.Combine(_dir, "test.txt")))
                File.Delete(Path.Combine(_dir, "test.txt"));
        }

        [Test]
        public void TestWaitForFileToAppear()
        {
            if (File.Exists(Path.Combine(_dir, "test.txt")))
                File.Delete(Path.Combine(_dir, "test.txt"));

            var options = new WaitFile.Parameters { ContinueIfExists = true, FilePath = _dir, FileMask = "test.*", TimeoutMS = 5000};

            var watchTask = Task.Run(() => WaitFile.WaitForFileToAppear(options, new CancellationToken()));
            Task.Delay(1000).Wait();
            File.WriteAllText(Path.Combine(_dir, "test.txt"), "test");
            watchTask.Wait();
            var result = watchTask.Result;

            Assert.AreEqual(Path.Combine(_dir, "test.txt"), result.FilePath);
        }

        [Test]
        public void TestWaitForFileToAppearTimeout()
        {
            if (File.Exists(Path.Combine(_dir, "test.txt")))
                File.Delete(Path.Combine(_dir, "test.txt"));

            var options = new WaitFile.Parameters { ContinueIfExists = true, FilePath = _dir, FileMask = "test.*", TimeoutMS = 2000 };
    
            var watchTask1 = Task.Run(() => WaitFile.WaitForFileToAppear(options, new CancellationToken()));
            Task.Delay(3000).Wait();
            File.WriteAllText(Path.Combine(_dir, "test.txt"), "test_line");
            watchTask1.Wait();
            var result = watchTask1.Result;

            Assert.AreEqual("", result.FilePath);
        }

        [Test]
        public void TestWaitForFileSkipExistingFile()
        {
            var options = new WaitFile.Parameters { ContinueIfExists = true, FilePath = _dir, FileMask = "test_file.txt", TimeoutMS = 2000 };

            var watchTask = Task.Run(() => WaitFile.WaitForFileToAppear(options, new CancellationToken()));
            watchTask.Wait();
            var result = watchTask.Result;

            Assert.IsTrue(result.FileExists);
        }
    }
}