using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.DemoRunner
{
    public class DemoRunner
    {

        String demoName;
        Dictionary<String, DemoOption> options = new Dictionary<String, DemoOption>();

        public DemoRunner(String DemoName)
        {
            this.demoName = DemoName;
        }

        private class DemoOption
        {
            public String OptionKey;
            public String Description;
            public Action Action;
        }

        public void AddOption(String OptionKey, String Description, Action Action)
        {
            var option = new DemoOption()
                            {
                                OptionKey = OptionKey,
                                Description = Description,
                                Action = Action
                            };
            this.options[OptionKey] = option;
        }

        public void Run()
        {
            Console.WriteLine(String.Format("**** {0} DEMO ****", this.demoName.ToUpper()));

            if (this.options.Count() == 0)
            {
                Console.WriteLine("\nNo options available - exiting.\n");
                return;
            }

            String response = "";
            DemoOption selectedOption;

            while (response.ToUpper() != "Q")
            {
                Console.WriteLine("\n\n\nSelect an option:\n\n");
                foreach (DemoOption o in this.options.Values)
                {
                    Console.WriteLine(String.Format("{0} - {1}", o.OptionKey, o.Description));
                }
                Console.WriteLine("\n ... or press Q to quit.\n\n");

                response = Console.ReadLine();

                try
                {
                    selectedOption = this.options[response];
                    Console.Clear();
                    selectedOption.Action();
                }
                catch (KeyNotFoundException e)
                {
                    if (response.ToUpper() != "Q")
                    {
                        Console.WriteLine("\nERROR - Option not available.\n");
                    }
                }

            }

        }

                    
            





    }
}
