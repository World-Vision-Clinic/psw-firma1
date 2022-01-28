using Integration.Pharmacy.Model;
using Integration.Pharmacy.Service;
using Integration_API.Controller;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests.UnitTests
{
    public class FilesUnitTests
    {
        [Fact]  // interaction with Rebex Client
        public void File_does_not_pdf_uploaded()
        {
            // Arrange
            SftpHandler sftp = new SftpHandler();

            // Act
            File downloaded = sftp.DownloadSpecification($"/public/SomeFile.txt", "Specifications/SomeFile.txt");

            // Assert
            Assert.Null(downloaded);
        }

        [Fact]  // interaction with Rebex Client
        public void File_is_uploaded()
        {
            // Arrange
            SftpHandler sftp = new SftpHandler();

            // Act
            File downloaded = sftp.DownloadSpecification($"/public/Aspirin.pdf", "Specifications/Aspirin.pdf");

            // Assert
            Assert.NotNull(downloaded);

        }

        [Fact]
        public void Check_get_file_paths_from_directory_when_there_is_two_files()
        {
            // Act
            string[] paths = FilesService.GetFilesPathsFromDirectory(@"../../../../IntegrationTests/TestHaveFiles");

            // Assert
            Assert.Equal(2, paths.Length);
        }

        [Fact]
        public void Check_get_file_paths_from_directory_when_there_is_no_files()
        {
            // Act
            string[] paths = FilesService.GetFilesPathsFromDirectory(@"../../../../IntegrationTests/TestHaveNoFiles");

            // Assert
            Assert.Empty(paths);
        }

        [Fact]
        public void Check_delete_files_method()
        {
            // Arrange
            System.IO.File.Create(@"../../../../IntegrationTests/DeletedFiles/Test1.txt").Dispose();
            System.IO.File.Create(@"../../../../IntegrationTests/DeletedFiles/Test2.txt").Dispose();
            System.IO.File.Create(@"../../../../IntegrationTests/DeletedFiles/Test3.txt").Dispose();
            string[] filePaths = { "../../../../IntegrationTests/DeletedFiles/Test1.txt",
                                   "../../../../IntegrationTests/DeletedFiles/Test2.txt",
                                   "../../../../IntegrationTests/DeletedFiles/Test3.txt"};

            // Act
            FilesService.DeleteFiles(filePaths);

            // Assert
            Assert.Empty(System.IO.Directory.GetFiles(@"../../../../IntegrationTests/DeletedFiles"));
        }

        [Fact]
        public void Check_compress_files_method()
        {
            // Arrange
            string source = "../../../../IntegrationTests/CompressFiles";
            string dest = "../../../../IntegrationTests/" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + ".zip";

            // Act
            FilesService.Compress(System.IO.Directory.GetFiles(source), dest);

            // Assert
            Assert.True(System.IO.File.Exists(dest));
        }
    }
}
