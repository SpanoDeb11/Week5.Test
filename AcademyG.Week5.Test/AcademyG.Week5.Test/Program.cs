using System;

namespace AcademyG.Week5.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int op; 

            do
            {
                Console.Clear();
                Console.WriteLine("Cosa vuoi fare?");
                Console.WriteLine("0) Stampa tutte le spese\n" +
                                  "1) Aggiungi nuova spesa\n" +
                                  "2) Approva una spesa già esistente\n" +
                                  "3) Elimina una spesa\n" +
                                  "4) Modifica una spesa\n" +
                                  "5) Elenco delle spese approvate\n" +
                                  "6) Elenco delle spese di uno specifico utente\n" +
                                  "7) Totale spese per ogni categoria\n" +
                                  "-) Exit");
                int.TryParse(Console.ReadLine(), out op);

                switch (op)
                {
                    case 0:
                        if (!EFOperations.PrintExpense())
                            Console.WriteLine("\nNon ci sono spese da stampare. Premi un tasto per tornare al menù");
                        else
                            Console.WriteLine("\nPremi un tasto per tornare al menù");
                        Console.ReadLine();
                        break;
                    case 1:
                        EFOperations.AddExpense();
                        break;
                    case 2:
                        EFOperations.ApproveExpense();
                        break;
                    case 3:
                        EFOperations.DeleteExpense();
                        break;
                    case 4:
                        EFOperations.ModifyExpense();
                        break;
                    case 5:
                        if (!EFOperations.PrintExpense(e => e.Approved))
                            Console.WriteLine("\nNon ci sono spese approvate da stampare. Premi un tasto per tornare al menù");
                        else
                            Console.WriteLine("\nPremi un tasto per tornare al menù");
                        Console.ReadLine();
                        break;
                    case 6:
                        Console.WriteLine("Inserisci l'utente di cui si vogliono cisualizzare le spese");
                        string user = Console.ReadLine();
                        if (!EFOperations.PrintExpense(e => e.User == user))
                            Console.WriteLine($"\nNon ci sono spese di {user} da stampare. Premi un tasto per tornare al menù");
                        else
                            Console.WriteLine("\nPremi un tasto per tornare al menù");
                        Console.ReadLine();
                        break;
                    case 7:
                        EFOperations.ListExpensePerCategory();
                        break;
                    default:
                        Console.WriteLine("Uscita in corso...");
                        break;
                }
            } while (op >= 0 && op <= 7);
        }
    }
}
