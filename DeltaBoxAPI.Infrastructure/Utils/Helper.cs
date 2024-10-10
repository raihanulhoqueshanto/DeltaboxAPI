using DeltaBoxAPI.Application.Common.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Infrastructure.Utils
{
    public class Helper
    {
        public static string GetSqlCondition(string conditionClause, string conditionOperator)
        {
            if (string.IsNullOrWhiteSpace(conditionClause))
            {
                return " WHERE ";
            }
            else if (conditionOperator.ToUpper() == "AND")
            {
                return " AND ";
            }
            else if (conditionOperator.ToUpper() == "OR")
            {
                return " OR ";
            }
            else
            {
                return "";
            }
        }

        public static async Task<Result> SaveSingleImage(string imageFile, string imagePathConstant, IHostEnvironment hostEnvironment)
        {
            var imagePath = ImageDirectory.CheckDirectory(hostEnvironment, imagePathConstant);
            if (imageFile != null || imageFile != "")
            {
                try
                {
                    int extentionStartIndex = imageFile.IndexOf('/');
                    int extensionEndIndex = imageFile.IndexOf(';');
                    //int filetypeStartIndex = imageFile.IndexOf(':');
                    //string fileType = imageFile.Substring(filetypeStartIndex + 1, extentionStartIndex-(filetypeStartIndex + 1));
                    string fileExtension = imageFile.Substring(extentionStartIndex + 1, extensionEndIndex - (extentionStartIndex + 1));

                    bool isSaved = false;

                    if (imageFile.Contains(","))
                    {
                        imageFile = imageFile.Substring(imageFile.IndexOf(",") + 1);
                    }

                    byte[] imageInBytes = Convert.FromBase64String(imageFile);

                    var imageName = string.Format(@"{0}." + fileExtension, Guid.NewGuid().ToString().Replace("-", ""));
                    isSaved = await ImageDirectory.SaveImageInDirectory(imageInBytes, imagePath, imageName);
                    if (!isSaved)
                    {
                        ImageDirectory.RemoveExistingFile(hostEnvironment, imageName, imagePathConstant);
                        return Result.Failure("Failed", "500", new[] { "Image Could Not Saved. Please try again!" }, null);
                    }
                    else
                    {
                        return Result.Success(imagePathConstant + imageName);
                    }
                }
                catch (Exception ex)
                {
                    return Result.Success(imagePathConstant + "default.png");
                }
            }
            else
            {
                return Result.Success(imagePathConstant + "default.png");
            }
        }
    }
}
