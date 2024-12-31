using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp13
{
    public class Presenter
    {
        private IView view;
        private IModel model;

        public Presenter(IView view, IModel model)
        {
            this.view = view;
            this.model = model;

            view.AddAuthorClicked += View_AddAuthorClicked;
            view.EditAuthorClicked += View_EditAuthorClicked;
            view.DeleteAuthorClicked += View_DeleteAuthorClicked;
            view.AddBookClicked += View_AddBookClicked;
            view.EditBookClicked += View_EditBookClicked;
            view.DeleteBookClicked += View_DeleteBookClicked;
            view.ShowAllBooksClicked += View_ShowAllBooksClicked;
            view.FilterByAuthorClicked += View_FilterByAuthorClicked;
            view.LoadDataClicked += View_LoadDataClicked;
            view.SaveDataClicked += View_SaveDataClicked;
            view.ExitClicked += View_ExitClicked;
            view.AuthorSelectedChanged += View_AuthorSelectedChanged;

            UpdateAuthorsList();
            UpdateBooksList(model.GetAllBooks());
        }

        private void View_AuthorSelectedChanged(object sender, EventArgs e)
        {
            var author = view.GetSelectedAuthor();
            if (author != null)
            {
                UpdateBooksList(author.Books);
            }
        }

        private void View_ExitClicked(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void View_SaveDataClicked(object sender, EventArgs e)
        {
            try
            {
                string filePath = view.ShowSaveFileDialog();
                model.SaveData(filePath);
                view.ShowMessage("Данные успешно сохранены.");
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }

        private void View_LoadDataClicked(object sender, EventArgs e)
        {
            try
            {
                string filePath = view.ShowOpenFileDialog();
                model.LoadData(filePath);
                UpdateAuthorsList();
                UpdateBooksList(model.GetAllBooks());
                view.ShowMessage("Данные успешно загружены.");
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }

        private void View_FilterByAuthorClicked(object sender, EventArgs e)
        {
            var author = view.GetSelectedAuthor();
            if (author == null)
            {
                view.ShowMessage("Выберите автора для фильтрации.");
                return;
            }
            UpdateBooksList(author.Books);
        }

        private void View_ShowAllBooksClicked(object sender, EventArgs e)
        {
            UpdateBooksList(model.GetAllBooks());
        }

        private void View_DeleteBookClicked(object sender, EventArgs e)
        {
            try
            {
                var book = view.GetSelectedBook();
                if (book == null)
                {
                    view.ShowMessage("Выберите книгу для удаления.");
                    return;
                }
                model.DeleteBook(book);
                UpdateBooksList(model.GetAllBooks());
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }

        private void View_AddAuthorClicked(object sender, EventArgs e)
        {
            try
            {
                Author newAuthor = new Author();
                if (view.ShowAuthorDialog(newAuthor, true))
                {
                    if (string.IsNullOrWhiteSpace(newAuthor.Name))
                    {
                        view.ShowError("Имя автора не может быть пустым.");
                        return;
                    }

                    model.AddAuthor(newAuthor.Name);
                    UpdateAuthorsList();
                }
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }

        private void View_AddBookClicked(object sender, EventArgs e)
        {
            try
            {
                var author = view.GetSelectedAuthor();
                if (author == null)
                {
                    view.ShowMessage("Выберите автора для добавления книги.");
                    return;
                }

                Book newBook = new Book();
                if (view.ShowBookDialog(newBook, model.GetAuthors(), true))
                {
                    if (string.IsNullOrWhiteSpace(newBook.Title))
                    {
                        view.ShowError("Название книги не может быть пустым.");
                        return;
                    }

                    model.AddBook(author, newBook.Title);
                    UpdateBooksList(author.Books);
                }
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }


        private void View_DeleteAuthorClicked(object sender, EventArgs e)
        {
            try
            {
                var author = view.GetSelectedAuthor();
                if (author == null)
                {
                    view.ShowMessage("Выберите автора для удаления.");
                    return;
                }
                model.DeleteAuthor(author);
                UpdateAuthorsList();
                UpdateBooksList(model.GetAllBooks());
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }

        private void View_EditAuthorClicked(object sender, EventArgs e)
        {
            try
            {
                var author = view.GetSelectedAuthor();
                if (author == null)
                {
                    view.ShowMessage("Выберите автора для редактирования.");
                    return;
                }

                // Показываем диалог редактирования
                if (view.ShowAuthorDialog(author, false))
                {
                    if (string.IsNullOrWhiteSpace(author.Name))
                    {
                        view.ShowError("Имя автора не может быть пустым.");
                        return;
                    }

                    model.EditAuthor(author, author.Name);
                    UpdateAuthorsList();
                }
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }

        private void View_EditBookClicked(object sender, EventArgs e)
        {
            try
            {
                var book = view.GetSelectedBook();
                if (book == null)
                {
                    view.ShowMessage("Выберите книгу для редактирования.");
                    return;
                }

                if (view.ShowBookDialog(book, model.GetAuthors(), false))
                {
                    if (string.IsNullOrWhiteSpace(book.Title))
                    {
                        view.ShowError("Название книги не может быть пустым.");
                        return;
                    }

                    model.EditBook(book, book.Title);
                    UpdateBooksList(model.GetAllBooks());
                }
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }


        

        private void UpdateAuthorsList()
        {
            view.UpdateAuthorList(model.GetAuthors());
        }

        private void UpdateBooksList(List<Book> books)
        {
            view.UpdateBookList(books);
        }
    }
}
