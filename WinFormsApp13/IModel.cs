using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp13
{
    public interface IModel
    {
        List<Author> GetAuthors();
        List<Book> GetAllBooks();
        void AddAuthor(string authorName);
        void EditAuthor(Author author, string newName);
        void DeleteAuthor(Author author);

        void AddBook(Author author, string bookTitle);
        void EditBook(Book book, string newTitle);
        void DeleteBook(Book book);

        void LoadData(string filePath);
        void SaveData(string filePath);
    }
}
