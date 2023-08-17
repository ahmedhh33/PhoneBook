namespace PhoonBook
{
    using Newtonsoft.Json;
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-|-|-|-|- Phone Book Application -|-|-|-|-");
            phoonBook phoonbook = new phoonBook();
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("Phone Book Program");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Search Contact");
                Console.WriteLine("3. Delete Contact");
                Console.WriteLine("4. Edit Contact");
                Console.WriteLine("5. Display Contact");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                int choice2;
                if (int.TryParse(Console.ReadLine(), out choice2))
                {
                    switch (choice2)
                    {
                        case 1:
                            
                            
                            phoonbook.AddContact(phoonbook.EntringName(),phoonbook.EntranceNumber());
                            break;
                        case 2:
                            
                            phoonbook.SearchContact(phoonbook.EntringName());
                            break;
                        case 3:
                            phoonbook.DeleteContact(phoonbook.EntringName());
                            break;
                        case 4:
                            
                            phoonbook.EditContact(phoonbook.EntringName(), phoonbook.EntranceNumber());
                            break;
                        case 5:
                            phoonbook.DisplayAllContacts();
                            break;
                        case 6:
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine();
            }
        }

        
    }
    public  class phoonBook
    {
        private string fileName = "phonebook.json";

        private static Dictionary<string, string> phoneBook = new Dictionary<string, string>();
        public phoonBook()
        {
            LoadContactsFromFile();
        }

        private void LoadContactsFromFile()
        {
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                phoneBook = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
        }
        private void SaveContactsToFile()
        {
            string json = JsonConvert.SerializeObject(phoneBook, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }
        public string EntringName()
        {
            try
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine();
                return name;
            } 
            catch (Exception e) 
            {
                Console.WriteLine("Invalid input exceptions the input must be integer " + e.Message);
            }
            return EntringName();
        }
        public string EntranceNumber() 
        {
            try
            {
                Console.Write("Enter Contact Number: ");
                string Number = Console.ReadLine();
                return Number;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input exceptions the input must be integer " + e.Message);
            }
            return EntranceNumber();
        }
        public  void AddContact(string name, string phoneNumber)
        {
            //Console.WriteLine("Adding Contact");
            

            if (phoneBook.ContainsKey(name))//name is kye so it must be uniqe
            {
                // if this contact exist ask user if he want to exist
                try
                {
                    Console.Write("This name is already exist Are you sure you want to updat? (Y/N)   ");
                    string response = Console.ReadLine().ToUpper();
                    if (response == "Y")
                    {
                        EditContact(name, phoneNumber);
                    }
                    else if (response == "N")
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You intered invalid choice");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input exceptions the input must be string " + ex.Message);

                }

            }
            else
            {
                
                phoneBook[name] = phoneNumber;//adding the value phoone number to the kye name
                SaveContactsToFile();
                Console.WriteLine("Contact added successfully.");
            }
        }
        public void SearchContact(string name)
        {
            //Console.WriteLine("Search Contact");
           

            if (phoneBook.TryGetValue(name, out string phoneNumber))
            {
                Console.WriteLine($"Phone Number: {phoneNumber}");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
        public void EditContact(string name, string newPhoonNumber)
        {
            //Console.WriteLine("Edit Contact");
            
            if (phoneBook.ContainsKey(name))
            {
                
                phoneBook[name] = newPhoonNumber;
                SaveContactsToFile();

                Console.WriteLine("The contact is updated");

            }
            else
            {
                Console.WriteLine($"The contact with name {name} not exist");
            }
        }
        public void DisplayAllContacts()
        {
            foreach(var contact in phoneBook)
            {
                Console.WriteLine($"Contact Name : {contact.Key} Contact Number : {contact.Value}");
            }
        }
        public void DeleteContact(string name)
        {
            
            

            if (phoneBook.Remove(name))
            {
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
            SaveContactsToFile();
        }

       
    

    }
}