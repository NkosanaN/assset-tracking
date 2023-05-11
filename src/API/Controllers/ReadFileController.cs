using System.Data;
using Domain;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadFileController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly DataContext _dataContext;
        private readonly ILogger<ReadFileController> _logger;
        public IExcelDataReader reader;
        public ReadFileController(IConfiguration config,
            IWebHostEnvironment hostEnvironment, DataContext dataContext, ILogger<ReadFileController> logger)
        {
            _config = config;
            _hostEnvironment = hostEnvironment;
            _dataContext = dataContext;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null)
                    return BadRequest("File is Not Received...");

                string dirPath = Path.Combine(_hostEnvironment.WebRootPath, "Received");

                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);

                string dataFileName = Path.GetFileName(file.FileName);

                string extension = Path.GetExtension(file.FileName);

                string[] allowedExtension = new string[] { ".xls", ".xlsx" };

                if (!allowedExtension.Contains(extension))
                    return BadRequest("Sorry This file is not allowed");

                string savePath = Path.Combine(dirPath, dataFileName);

                await using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                // USe this to handle Encodeing differences in .NET Core
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                await using (var stream = new FileStream(savePath, FileMode.Open))
                {
                    if (extension == ".xls")
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    else
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                    var ds = reader.AsDataSet();
                    reader.Close();

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable serviceDetails = ds.Tables[0];

                        for (int i = 1; i < serviceDetails.Rows.Count; i++)
                        {
                            bool dueforrepair = Convert.ToInt32(serviceDetails.Rows[i][3].ToString()) == 1;

                            Item item = new()
                            {
                                Name = serviceDetails.Rows[i][0].ToString(),
                                Description = serviceDetails.Rows[i][1].ToString(),
                                Qty = Convert.ToInt64(serviceDetails.Rows[i][2].ToString()),
                                DueforRepair = dueforrepair
                            };
                            //await _dataContext.AddAsync(item);
                            //await _dataContext.SaveChangesAsync();
                        }
                    }
                }
                _logger.LogInformation("File uploaded successfully");
                return Ok("File uploaded successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
            }
            _logger.LogInformation("File uploaded successfully");
            return BadRequest("Fail to upload file");
        }
    }
}
