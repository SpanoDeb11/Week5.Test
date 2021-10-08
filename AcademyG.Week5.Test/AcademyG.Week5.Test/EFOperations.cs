using AcademyG.Week5.Test.Context;
using AcademyG.Week5.Test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademyG.Week5.Test
{
    public static class EFOperations
    {
        public static void AddExpense()
        {
            bool flag; //usato per il check del tryparse del decimal

            Console.Clear();
            Console.WriteLine("Inserire la descrizione della spesa");
            string description = Console.ReadLine();
            Console.WriteLine("Inserire l'utente che effettua la spesa");
            string user = Console.ReadLine();
            decimal amount;
            do
            {
                //chiede di inserire l'importo finchè il tryparse non restituisce true
                Console.WriteLine("Inserire l'importo della spesa");
                flag = decimal.TryParse(Console.ReadLine(), out amount);
                if (!flag)
                {
                    Console.WriteLine("Errore nell'inserimento dell'importo. Riprovare!");
                }
            } while (!flag);


            Console.WriteLine("Inserire la categoria della spesa");
            string categoryType = Console.ReadLine();

            using (ExpensesContext ctx = new ExpensesContext())
            {
                var category = ctx.Categories.FirstOrDefault(
                    c => c.Type.ToUpper() == categoryType.ToUpper());

                Expense newExpense = new Expense()
                {
                    Date = DateTime.Now,
                    Description = description,
                    User = user,
                    Amount = amount,
                    Approved = false,
                    Category = category
                };

                Console.WriteLine("Vuoi aggiungere la seguente spesa? (y or n)");
                Console.WriteLine($"{newExpense.Description} {newExpense.Date} {newExpense.User} {newExpense.Amount} {newExpense.Category?.Type}");
                string s = Console.ReadLine();

                if (s == "y")
                {
                    ctx.Expenses.Add(newExpense);
                    ctx.SaveChanges();
                    Console.WriteLine("Spesa inserita con successo\n");
                }
                else
                {
                    Console.WriteLine("Operazione annullata");
                }
            }

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadLine();
        }

        public static void ApproveExpense()
        {
            bool flag; //usato per il check del tryparse

            if (!PrintExpense(e => !e.Approved))
            {
                Console.WriteLine("\nNessuno spesa da approvare! Premi un tasto per tornare al menù");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nInserire l'id della spesa che si vuole approvare");
            int id;
            using (ExpensesContext ctx = new ExpensesContext())
            {
                Expense expense = new Expense();
                do
                {
                    flag = int.TryParse(Console.ReadLine(), out id);
                    if (!flag)
                    {
                        Console.WriteLine("Errore. L'id inserito non è valido. Riprova");
                        continue;
                    }
                    
                    expense = ctx.Expenses.Find(id);
                    if(expense == null || expense.Approved)
                    {
                        Console.WriteLine("Errore l'id inserito non esiste o appartiene ad una spesa già approvata. Riprova");
                        flag = false;
                        continue;
                    }
                } while (!flag);

                Console.WriteLine("\nVuoi approvare la seguente spesa? (y or n)");
                Console.WriteLine($"{expense.Description} {expense.Date} {expense.User} {expense.Amount} {expense.Category?.Type}");
                string s = Console.ReadLine();

                if (s == "y")
                {
                    expense.Approved = true;
                    ctx.SaveChanges();
                    Console.WriteLine("Spesa Approvata");
                }
                else
                {
                    Console.WriteLine("Operazione annullata");
                }
            }

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadLine();
        }

        public static void DeleteExpense()
        {
            bool flag; //flag per il check del tryparse

            if (!PrintExpense())
            {
                Console.WriteLine("\nNessuno spesa da eliminare! Premi un tasto per tornare al menù");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nInserire l'id della spesa che si vuole eliminare");
            int id;

            using (ExpensesContext ctx = new ExpensesContext())
            {
                Expense expense = new Expense();
                do
                {
                    flag = int.TryParse(Console.ReadLine(), out id);
                    if (!flag)
                    {
                        Console.WriteLine("Errore. L'id inserito non è valido. Riprova");
                        continue;
                    }

                    expense = ctx.Expenses.Find(id);
                    if (expense == null)
                    {
                        Console.WriteLine("Errore l'id inserito non esiste Riprova");
                        flag = false;
                        continue;
                    }
                } while (!flag);

                Console.WriteLine("\nVuoi eliminare la seguente spesa? (y or n)");
                Console.WriteLine($"{expense.Description} {expense.Date} {expense.User} {expense.Amount} {expense.Category?.Type}");
                string s = Console.ReadLine();

                if (s == "y")
                {
                    ctx.Expenses.Remove(expense);
                    ctx.SaveChanges();
                    Console.WriteLine("Spesa Eliminata");
                }
                else
                {
                    Console.WriteLine("Operazione annullata");
                }
            }

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadLine();
        }

        public static void ModifyExpense()
        {
            bool flag; //usato per il check del tryparse

            if (!PrintExpense())
            {
                Console.WriteLine("\nNessuno spesa da modificare! Premi un tasto per tornare al menù");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nInserire l'id della spesa che si vuole modificare");
            int id;
            using (ExpensesContext ctx = new ExpensesContext())
            {
                Expense expense = new Expense();
                do
                {
                    flag = int.TryParse(Console.ReadLine(), out id);
                    if (!flag)
                    {
                        Console.WriteLine("Errore. L'id inserito non è valido. Riprova");
                        continue;
                    }

                    expense = ctx.Expenses.Find(id);
                    if (expense == null)
                    {
                        Console.WriteLine("Errore l'id inserito non esiste. Riprova");
                        flag = false;
                        continue;
                    }
                } while (!flag);

                Console.WriteLine("\nVuoi Modificare la seguente spesa? (y or n)");
                Console.WriteLine($"{expense.Description} {expense.Date} {expense.User} {expense.Amount} {expense.Category?.Type}");
                string s = Console.ReadLine();

                if (s != "y") //operazione annullata esco subito
                {
                    Console.WriteLine("Operazione annullata");
                    Console.WriteLine("Premi un tasto per continuare");
                    Console.ReadLine();
                    return;
                }

                //continuo operazione
                Console.WriteLine("Inserire la nuova descrizione");
                string description = Console.ReadLine();
                Console.WriteLine("Inserire il nuovo utente");
                string user = Console.ReadLine();
                decimal amount;
                do
                {
                    //chiede di inserire l'importo finchè il tryparse non restituisce true
                    Console.WriteLine("Inserire il nuovo importo");
                    flag = decimal.TryParse(Console.ReadLine(), out amount);
                    if (!flag)
                    {
                        Console.WriteLine("Errore nell'inserimento dell'importo. Riprovare!");
                    }
                } while (!flag);

                DateTime date;
                do
                {
                    //chiede di inserire la data finchè il tryparse non restituisce true
                    Console.WriteLine("Inserire la nuova data");
                    flag = DateTime.TryParse(Console.ReadLine(), out date);
                    if (!flag)
                    {
                        Console.WriteLine("Errore nell'inserimento della data. Riprovare!");
                    }
                } while (!flag);


                Console.WriteLine("Inserire la categoria della spesa");
                string categoryType = Console.ReadLine();

                var category = ctx.Categories.FirstOrDefault(
                    c => c.Type.ToUpper() == categoryType.ToUpper());

                expense.Description = description;
                expense.Date = date;
                expense.User = user;
                expense.Amount = amount;
                expense.Category = category;
                
                ctx.SaveChanges();
                Console.WriteLine("Spesa Modificata");
            }

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadLine();
        }

        public static void ListExpensePerCategory()
        {
            using ExpensesContext ctx = new ExpensesContext();

            Console.Clear();
            foreach (var item in ctx.Categories.Include(c => c.Expenses))
                Console.WriteLine($"{item.Type} - {item.Expenses.Count()}");

            Console.WriteLine("\nPremi un tasto per continuare");
            Console.ReadLine();
        }

        public static bool PrintExpense(Func<Expense, bool> filter = null)
        {
            using (ExpensesContext ctx = new ExpensesContext())
            {
                IEnumerable<Expense> expenses;

                if (filter == null)
                    expenses = ctx.Expenses
                        .Include(e => e.Category);
                else
                    expenses = ctx.Expenses
                        .Include(e => e.Category)
                        .Where(filter);

                if(expenses.Count() == 0)
                    return false; // se la lista è vuola restituisce false

                Console.Clear();
                foreach (var e in expenses)
                    Console.WriteLine($"[{e.Id}] {e.Description} {e.Date} {e.User} {e.Amount} {e.Category?.Type} {(e.Approved ? "Approvata" : "Non Approvata")}");

                return true;
            }
        }
    }
}
