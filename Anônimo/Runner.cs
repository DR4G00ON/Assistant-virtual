using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anônimo
{
    public class Runner
    {

        // Fala que horas são
        public static void WhatTimesIs()
        {
            // Esse comando vai falar as horas
            Speaker.Speak(DateTime.Now.ToShortTimeString());
        }
        public static void WhatDateIs()
        {
            Speaker.Speak(DateTime.Now.ToShortDateString());
        }
        public static string[] answer = new string[] { "Olá chef, tudo bem", "Boa noite chef", "Boa tarde chef", "Bom dia chef" };
        public static void SpeakAnswer(int x)
        {
            Speaker.Speak(answer[x]);
        }
    }
}
