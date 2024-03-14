using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anônimo
{
    public class GrammarRules
    {

        // Reponsavél por pedir as horas
        public static IList<string> WhatTimeIs = new List<string>()
        {
            "que horas são",
            "Me diga as horas",
            "Poderia me dizer que horas são"
        };


        // Comandos de voz para saber a data atual
        public static IList<string> WhatDateIS = new List<string>()
        {
            "Data de hoje",
            "Que dia é hoje",
            "Hoje é que dia"
        };

        // Comandos de voz para chamar o assistente caso ela esteja calada
        public static IList<string> AnonimoStartListerning = new List<string>()
        {
            "Anônimo",
            "Olá Anônimo"
        };


        // Comandos de voz para a assistente ficar calada
        public static IList<string> AnonimoStopListerning = new List<string>()
        {
            "Pare de ouvir",
            "Pare de me ouvir",
            "Silêncio",
            "Cala a boca"
        };


        // Comandos de voz para a tela ficar minimizada
        public static IList<string> MinimizeWindow = new List<string>()
        {
            "Minimizar janela",
            "Minimizar tela",
            "MInimize a tela"
        };


        // Comandos de voz para deixa a tela no tamanho normal
        public static IList<string> NormalWindow = new List<string>()
        {
            "Deixe a tela no normal",
            "Tela normal",
            "Tamanho normal",
        };



        // Comados de voz de conversas paralelas 
        public static string[] conversations = new string[] {"Olá", "boa noite", "boa tarde", "bom dia", "Você estar bem",
        "tô bem", };

        // Comandos de voz de abrir aplicativo
        public static string[] openApps = new string[] { "Abrir visual studio 2019" };


        // Comandos de voz responsavél por abrir sties webs
        public static string[] openWebs = new string[]
        {
            "Abrir youtube",
            "Abrir tradutor"
        };


        // Comandos de voz para fechar o aplicativo
        public static string[] closeApp = new string[]
        {
            "Tchau",
            "Anônimo vai descansar"
        };
    }
}
