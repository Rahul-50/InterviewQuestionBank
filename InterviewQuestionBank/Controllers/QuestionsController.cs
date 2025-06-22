using Microsoft.AspNetCore.Mvc;
using System.Data;
//using System.Data.SqlClient;
using InterviewQuestionBank.Models;
using Microsoft.Data.SqlClient;

public class QuestionsController : Controller
{
    private readonly DbHelper _db;

    public QuestionsController(IConfiguration config)
    {
        _db = new DbHelper(config);
    }

    public IActionResult Index()
    {
        var dt = _db.GetData("SELECT Q.Id, Q.QuestionText, Q.Difficulty, C.Name AS CategoryName FROM Questions Q JOIN Categories C ON Q.CategoryId = C.Id");

        var list = new List<Question>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new Question
            {
                Id = Convert.ToInt32(row["Id"]),
                QuestionText = row["QuestionText"].ToString(),
                Difficulty = row["Difficulty"].ToString(),
                CategoryName = row["CategoryName"].ToString()
            });
        }

        return View(list);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = GetCategories();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Question q)
    {
        string query = "INSERT INTO Questions (QuestionText, Answer, Difficulty, CategoryId) VALUES (@text, @ans, @diff, @catId)";
        var parameters = new SqlParameter[]
        {
            new("@text", q.QuestionText),
            new("@ans", q.Answer),
            new("@diff", q.Difficulty),
            new("@catId", q.CategoryId)
        };

        _db.Execute(query, parameters);
        return RedirectToAction("Index");
    }
    public IActionResult Delete(int id)
    {
        string query = "DELETE FROM Questions WHERE Id = @id";
        var parameters = new SqlParameter[]
        {
        new SqlParameter("@id", id)
        };
        _db.Execute(query, parameters);

        return RedirectToAction("Index");
    }
    //
    private List<Category> GetCategories()
    {
        //var dt = _db.GetData("SELECT * FROM Categories");
        //var dt = _db.GetData("SELECT Id,Name FROM Categories");
        var dt = _db.GetData("SELECT Id,Name FROM Categories");
        var list = new List<Category>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new Category
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString()
            });
        }
        return list;
    }
}
