using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Dto;
using Integration_API.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        NewsService newsService = new NewsService(new NewsRepository());

        [HttpGet]
        public IActionResult Get()
        {
            List<News> news = new List<News>();
            List<NewsDto> result = new List<NewsDto>();
            news = newsService.GetAll();
            news.ForEach(pieceOfNews => result.Add(NewsMapper.NewsToNewsDto(pieceOfNews)));
            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateNews(NewsDto dto)
        {
            if (dto.Id.Length <= 0 || dto.Content.Length <= 0)
            {
                return BadRequest();
            }

            List<News> news = newsService.GetAll();
            News pieceOfNews = news.Find(pieceOfNews => pieceOfNews.IdEncoded == dto.Id);

            if (pieceOfNews == null)
            {
                return NotFound();
            }
            else
            {
                newsService.Update(pieceOfNews);
                return Ok(NewsMapper.NewsToNewsDto(pieceOfNews));
            }
        }

        [HttpGet("test")]
        public IActionResult TestingController()
        {
            return Ok("Hello from News controller");
        }
    }

}
