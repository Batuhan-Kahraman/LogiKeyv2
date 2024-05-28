using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Migrations;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Patagames.Ocr.Enums;
using Patagames.Ocr;
using Tesseract;
using System.Reflection.Metadata;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Windows.Media.Ocr;
using Windows.Storage;
using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using Google.Cloud.Vision.V1;
using Google.Protobuf;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Protobuf;
using Newtonsoft.Json;
using System.Security.Cryptography;


namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class EFaturaController : BaseController
    {
        FirmaManager firmaManager = new FirmaManager(new EFFirmaRepository());
        public IActionResult FaturaGonder(int FaturaID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");


            //efatura gönder

           var sonucFatura = Task.Run(async () => await EFaturaHelper.Create(FaturaID, FirmaID)).Result;


            ViewBag.mesaj = sonucFatura;
            return View();
        }
       


    }
}