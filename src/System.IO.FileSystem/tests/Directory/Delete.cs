// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Xunit;

namespace System.IO.FileSystem.Tests
{
    public class Directory_Delete_str : FileSystemTest
    {
        #region Utilities

        public virtual void Delete(string path)
        {
            Directory.Delete(path);
        }

        #endregion

        #region UniversalTests

        [Fact]
        public void NullParameters()
        {
            Assert.Throws<ArgumentNullException>(() => Delete(null));
        }

        [Fact]
        public void InvalidParameters()
        {
            Assert.Throws<ArgumentException>(() => Delete(string.Empty));
        }

        [Fact]
        public void ShouldThrowIOExceptionIfContainedFileInUse()
        {
            DirectoryInfo testDir = Directory.CreateDirectory(GetTestFilePath());
            using (File.Create(Path.Combine(testDir.FullName, GetTestFileName())))
            {
                Assert.Throws<IOException>(() => Delete(testDir.FullName));
            }
            Assert.True(testDir.Exists);
        }

        [Fact]
        public void ShouldThrowIOExceptionForDirectoryWithFiles()
        {
            DirectoryInfo testDir = Directory.CreateDirectory(GetTestFilePath());
            File.Create(Path.Combine(testDir.FullName, GetTestFileName())).Dispose();
            Assert.Throws<IOException>(() => Delete(testDir.FullName));
            Assert.True(testDir.Exists);
        }

        [Fact]
        public void DirectoryWithSubdirectories()
        {
            DirectoryInfo testDir = Directory.CreateDirectory(GetTestFilePath());
            testDir.CreateSubdirectory(GetTestFileName());
            Assert.Throws<IOException>(() => Delete(testDir.FullName));
            Assert.True(testDir.Exists);
        }

        [Fact]
        [OuterLoop]
        public void DeleteRoot()
        {
            Assert.Throws<IOException>(() => Delete(Path.GetPathRoot(Directory.GetCurrentDirectory())));
        }

        [Fact]
        public void PositiveTest()
        {
            DirectoryInfo testDir = Directory.CreateDirectory(GetTestFilePath());
            Delete(testDir.FullName);
            Assert.False(testDir.Exists);
        }

        [Fact]
        public void ShouldThrowDirectoryNotFoundExceptionForNonexistentDirectory()
        {
            Assert.Throws<DirectoryNotFoundException>(() => Delete(GetTestFilePath()));
        }

        [Fact]
        public void ShouldThrowIOExceptionDeletingCurrentDirectory()
        {
            Assert.Throws<IOException>(() => Delete(Directory.GetCurrentDirectory()));
        }

        #endregion

        #region PlatformSpecific

        [Fact]
        [PlatformSpecific(PlatformID.Windows)]
        public void WindowsExtendedDirectoryWithSubdirectories()
        {
            DirectoryInfo testDir = Directory.CreateDirectory(@"\\?\" + GetTestFilePath());
            testDir.CreateSubdirectory(GetTestFileName());
            Assert.Throws<IOException>(() => Delete(testDir.FullName));
            Assert.True(testDir.Exists);
        }

        [Fact]
        [PlatformSpecific(PlatformID.Windows)]
        public void WindowsDeleteReadOnlyDirectory()
        {
            DirectoryInfo testDir = Directory.CreateDirectory(GetTestFilePath());
            testDir.Attributes = FileAttributes.ReadOnly;
            Assert.Throws<IOException>(() => Delete(testDir.FullName));
            Assert.True(testDir.Exists);
            testDir.Attributes = FileAttributes.Normal;
        }

        [Fact]
        [PlatformSpecific(PlatformID.Windows)]
        public void WindowsDeleteExtendedReadOnlyDirectory()
        {
            DirectoryInfo testDir = Directory.CreateDirectory(@"\\?\" + GetTestFilePath());
            testDir.Attributes = FileAttributes.ReadOnly;
            Assert.Throws<IOException>(() => Delete(testDir.FullName));
            Assert.True(testDir.Exists);
            testDir.Attributes = FileAttributes.Normal;
        }

        [Fact]
        [PlatformSpecific(PlatformID.AnyUnix)]
        public void UnixDeleteReadOnlyDirectory()
        {
            DirectoryInfo testDir = Directory.CreateDirectory(GetTestFilePath());
            testDir.Attributes = FileAttributes.ReadOnly;
            Delete(testDir.FullName);
            Assert.False(testDir.Exists);
        }

        [Fact]
        [PlatformSpecific(PlatformID.Windows)]
        public void WindowsShouldBeAbleToDeleteHiddenDirectory()
        {
            DirectoryInfo testDir = Directory.CreateDirectory(GetTestFilePath());
            testDir.Attributes = FileAttributes.Hidden;
            Delete(testDir.FullName);
            Assert.False(testDir.Exists);
        }

        [Fact]
        [PlatformSpecific(PlatformID.Windows)]
        public void WindowsShouldBeAbleToDeleteExtendedHiddenDirectory()
        {
            DirectoryInfo testDir = Directory.CreateDirectory(@"\\?\" + GetTestFilePath());
            testDir.Attributes = FileAttributes.Hidden;
            Delete(testDir.FullName);
            Assert.False(testDir.Exists);
        }

        [Fact]
        [PlatformSpecific(PlatformID.AnyUnix)]
        public void UnixShouldBeAbleToDeleteHiddenDirectory()
        {
            string testDir = "." + GetTestFileName();
            Directory.CreateDirectory(Path.Combine(TestDirectory, testDir));
            Assert.True(0 != (new DirectoryInfo(Path.Combine(TestDirectory, testDir)).Attributes & FileAttributes.Hidden));
            Delete(Path.Combine(TestDirectory, testDir));
            Assert.False(Directory.Exists(testDir));
        }

        #endregion
    }

    public class Directory_Delete_str_bool : Directory_Delete_str
    {
        #region Utilities

        public override void Delete(string path)
        {
            Directory.Delete(path, false);
        }

        public virtual void Delete(string path, bool recursive)
        {
            Directory.Delete(path, recursive);
        }

        #endregion

        [Fact]
        public void RecursiveDelete()
        {
            DirectoryInfo testDir = Directory.CreateDirectory(GetTestFilePath());
            File.Create(Path.Combine(testDir.FullName, GetTestFileName())).Dispose();
            testDir.CreateSubdirectory(GetTestFileName());
            Delete(testDir.FullName, true);
            Assert.False(testDir.Exists);
        }

    }
}
