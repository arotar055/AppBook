using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp13
{
    public interface IView
    {
        event EventHandler? AddAuthorClicked;
        event EventHandler? EditAuthorClicked;
        event EventHandler? DeleteAuthorClicked;
        event EventHandler? AddBookClicked;
        event EventHandler? EditBookClicked;
        event EventHandler? DeleteBookClicked;
        event EventHandler? ShowAllBooksClicked;
        event EventHandler? FilterByAuthorClicked;
        event EventHandler? LoadDataClicked;
        event EventHandler? SaveDataClicked;
        event EventHandler? ExitClicked;
        event EventHandler? AuthorSelectedChanged;

        void UpdateAuthorList(List<Author> authors);
        void UpdateBookList(List<Book> books);
        Author GetSelectedAuthor();
        Book GetSelectedBook();

        bool ShowAuthorDialog(Author author, bool isAddMode);
        bool ShowBookDialog(Book book, List<Author> authors, bool isNew);

        void ShowMessage(string message);
        void ShowError(string errorMessage);
        string ShowOpenFileDialog();
        string ShowSaveFileDialog();
    }
}
