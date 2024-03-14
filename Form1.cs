using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition; // adicionar namespace
using System.IO;


// Para sintesí é preciso o SpeechSDK5.1, Windows 10 System.speech

namespace Anônimo
{
    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine engine; // engine de recohencimento
        private bool isAnonimoListerning = true;




        public Form1()
        {
            InitializeComponent();
        }
        private void LoadSpeech()
        {
            try
            {
            GrammarRules gm = new GrammarRules();




                engine = new SpeechRecognitionEngine(); // instância
                engine.SetInputToDefaultAudioDevice(); // Microfone
                Choices c_commandsOdSystem = new Choices();
                c_commandsOdSystem.Add(GrammarRules.WhatTimeIs.ToArray()); // whatTimeIs
                c_commandsOdSystem.Add(GrammarRules.WhatDateIS.ToArray()); // WhatDateIs
                c_commandsOdSystem.Add(GrammarRules.conversations.ToArray()); // Conversation
                c_commandsOdSystem.Add(GrammarRules.AnonimoStartListerning.ToArray()); //Anônimo ouve
                c_commandsOdSystem.Add(GrammarRules.AnonimoStopListerning.ToArray()); // Anônimo não ouve
                c_commandsOdSystem.Add(GrammarRules.openApps.ToArray()); // abertura de apps
                c_commandsOdSystem.Add(GrammarRules.MinimizeWindow.ToArray()); // minimizador da tela
                c_commandsOdSystem.Add(GrammarRules.NormalWindow.ToArray()); // deixa a tela no tamanho normal
                c_commandsOdSystem.Add(GrammarRules.openWebs.ToArray()); // Irar abrir sites webs
                c_commandsOdSystem.Add(GrammarRules.closeApp.ToArray()); // Irar fechar o anônimo

                //"pare de ouvir" -> "anônimo"

                GrammarBuilder gb_commandsOfSystem = new GrammarBuilder();
                gb_commandsOfSystem.Append(c_commandsOdSystem);

                Grammar g_commandsOfSystem = new Grammar(gb_commandsOfSystem);
                g_commandsOfSystem.Name = "sys";


                engine.LoadGrammar(g_commandsOfSystem); // carregar gramática

                // carregar a gramática 
                //engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(words))));

                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(rec);
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(audioLevel);
                engine.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(rej);

                engine.RecognizeAsync(RecognizeMode.Multiple); //iniciar o reconhecimento
                Speaker.Speak("Estou carregando os arquivos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu erro no LoadSpeech(): {0}", ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSpeech();
            Speaker.Speak("Já carreguei os arquivos, estou pronta!");
        }

        // método que é chamado quando algo é reconhecido
        private void rec(object s, SpeechRecognizedEventArgs e)
        {
            string youtube = "https://www.youtube.com/"; // endereço web do youtube
            string tradutor = "https://translate.google.com/"; // endereço web do tradutor
            string speech = e.Result.Text; // string reconhecida
            float conf = e.Result.Confidence;

            string date = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
            string log_filename = "log\\" + date + ".txt";


            Runner r = new Runner();
            StreamWriter sw = File.AppendText(log_filename);
            if (File.Exists(log_filename))
                sw.WriteLine(speech);
            else
            {
                sw.WriteLine(speech);
            }
            sw.Close();

            if (conf > 0.35f)
            {
                this.label1.BackColor = Color.Blue;
                this.label1.ForeColor = Color.White;

                if ( GrammarRules.AnonimoStopListerning.Any(x => x == speech))
                {
                    isAnonimoListerning = false;
                }
                else if(GrammarRules.AnonimoStartListerning.Any(x => x == speech))
                {
                    isAnonimoListerning = true;
                }
                if(isAnonimoListerning == true)
                {
                    this.label1.Text = "Reconhecido";
                    switch (e.Result.Grammar.Name)
                    {
                        case "sys":

                            // se o speech == "Que horas são"
                            if (GrammarRules.WhatTimeIs.Any(x => x == speech))
                            {
                                Runner.WhatTimesIs();
                            }
                            else if (GrammarRules.WhatDateIS.Any(x => x == speech))
                            {
                                Runner.WhatDateIs();
                            }
                            else if (speech == "Olá")
                            {
                                Runner.SpeakAnswer(0);
                            }
                            else if (speech == "boa noite")
                            {
                                Runner.SpeakAnswer(1);
                            }
                            else if (speech == "boa tarde")
                            {
                                Runner.SpeakAnswer(2);
                            }
                            else if (speech == "bom dia")
                            {
                                Runner.SpeakAnswer(3);
                            }
                            else if (speech == "Você estar bem")
                            {
                                Speaker.Speak("Estou ótima", "sim", "Estou bem", "Tô bem e o senhor?");
                            }
                            else if (speech == "tô bem")
                            {
                                Speaker.Speak("Que bom", "Que maravilha", "ótimo", "Que bom, O que deseja chef?");
                            }
                            else if (speech == "Abrir visual studio 2019")
                            {
                                Speaker.Speak("Ok estou abrindo");
                                System.Diagnostics.Process.Start
                                (@"c:\ProgramData\Microsoft\Windows\Start Menu\Programs\Visual Studio 2019");
                            }
                            else if (GrammarRules.MinimizeWindow.Any(x => x == speech))
                            {
                                MinimizeWindow();
                            }
                            else if (GrammarRules.NormalWindow.Any(x => x == speech))
                            {
                                NormalWindow();
                            }
                            else if(speech == "Abrir youtube")
                            {
                                Speaker.Speak("Ok", "Estou abrindo", "Abrindo Youtube", "Abrindo", "Como quiser");
                                System.Diagnostics.Process.Start(youtube);
                            }
                            else if(speech == "Abrir tradutor")
                            {
                                Speaker.Speak("Ok", "Estou abrindo", "Abrindo Tradutor", "Abrindo", "Como quiser");
                                System.Diagnostics.Process.Start(tradutor);
                            }
                            else if(speech == "Anônimo vai descansar")
                            {
                                Speaker.Speak("OK chef", "Obrigrado", "OK");
                                System.Threading.Thread.Sleep(4000);
                                Close();
                            }
                            else if(speech == "Tchau")
                            {
                                Speaker.Speak("Tchau chef", "Tchau", "Tchau, até logo");
                                Console.ReadLine();
                                System.Threading.Thread.Sleep(4000);
                                Close();
                            }
                            break;

                            
                       
                    }
                }
            }
        }
        private void audioLevel(object s, AudioLevelUpdatedEventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.AudioLevel;
        }
        private void rej(object s, SpeechRecognitionRejectedEventArgs e)
        {
            this.label1.BackColor = Color.Black;
            this.label1.ForeColor = Color.White;
            this.label1.Text = "Inrenconhecivél";
        }

        // minimizar janela
        private void MinimizeWindow()
        {
            if(this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Minimized;
                Speaker.Speak("minimizando a janela", "como quiser", "tudo bem", "minimizando", "vou fazer isso");
            }
            else
            {
                Speaker.Speak("A tela já estar minimizada");
            }
        }
        private void NormalWindow()
        {
            if(this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                Speaker.Speak("Deixando a tela normal", "Como quiser", "Estou fazendo", "vou fazer isso", "Tela normal");
            }
            else
            {
                Speaker.Speak("A tela já estar no tamanho normal");
            }
        }
    }

}
