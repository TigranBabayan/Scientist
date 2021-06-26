using ClosedXML.Excel;
using Ganss.Excel;
using Microsoft.AspNetCore.Mvc;
using scientist.Models;
using System.IO;
using System.Linq;

namespace scientist.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationContext _context;
        public AdminController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Export()
        {
            //ներբեռնում ենք Excel ֆորմատով
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Scientist");
                var currenRow = 1;
                // worksheet.Cell(currenRow, 1).Value = "Id";
                worksheet.Cell(currenRow, 2).Value = "FullName";
                worksheet.Cell(currenRow, 3).Value = "Birthday";
                worksheet.Cell(currenRow, 4).Value = "Cityzenship";
                worksheet.Cell(currenRow, 5).Value = "Profession";
                worksheet.Cell(currenRow, 6).Value = "Workplace";
                worksheet.Cell(currenRow, 7).Value = "Livingplace";
                worksheet.Cell(currenRow, 8).Value = "Degree";
                worksheet.Cell(currenRow, 9).Value = "Adress";
                worksheet.Cell(currenRow, 10).Value = "Phone";
                worksheet.Cell(currenRow, 11).Value = "Email";
                //worksheet.Cell(currenRow, 12).Value = "OperativTvyalner";
                //worksheet.Cell(currenRow, 13).Value = "BacAxbyur";
                worksheet.Cell(currenRow, 14).Value = "Other";


                var scientists = _context.Scientist.ToList();
                foreach (var scientist in scientists)
                {
                    currenRow++;
                    // worksheet.Cell(currenRow, 1).Value = scientist.Id;
                    worksheet.Cell(currenRow, 2).Value = scientist.FullName;
                    worksheet.Cell(currenRow, 3).Value = scientist.Birthday;
                    worksheet.Cell(currenRow, 4).Value = scientist.Cityzenship;
                    worksheet.Cell(currenRow, 5).Value = scientist.Profession;
                    worksheet.Cell(currenRow, 6).Value = scientist.Workplace;
                    worksheet.Cell(currenRow, 7).Value = scientist.Livingplace;
                    worksheet.Cell(currenRow, 8).Value = scientist.Degree;
                    worksheet.Cell(currenRow, 9).Value = scientist.Adress;
                    worksheet.Cell(currenRow, 10).Value = scientist.Phone;
                    worksheet.Cell(currenRow, 11).Value = scientist.Email;
                    //worksheet.Cell(currenRow, 12).Value = scientist.OperativTvyalner;
                    //worksheet.Cell(currenRow, 13).Value = scientist.BacAxbyur;
                    worksheet.Cell(currenRow, 14).Value = scientist.Other;

                }
                using (var stream = new MemoryStream())
                {
                    //var ymd = DateTime.Now.ToString("dd-MM-yyyy");
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        /*ymd +*/ "Scientists.xlsx"
                        );
                }
            }

        }


        public IActionResult Import()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            //Վերականգնում ենք բազան
            //օգտվել եմ Youtube-ից https://www.youtube.com/watch?v=MUQ4eRc98Iw
            //using Ganss.Excel;

            string fileName = "D:\\Scientists\\Scientists.xlsx";

            var scientists = new ExcelMapper(fileName).Fetch<Scientist>();
            foreach (var scientist in scientists)
            {
                _context.Scientist.Add(scientist);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Scientists");
        }

    }

}