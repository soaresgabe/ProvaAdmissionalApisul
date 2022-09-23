using ProvaAdmissionalCSharpApisul;
using System;
using System.Collections.Generic;
using ElevatorProject;
using System.Linq;
using System.Diagnostics.Metrics;

namespace ElevatorProject
{
    public class Process : IElevadorService
    {
        List<Data> answers = new List<Data>();
        List<int> andares = new List<int>();
        List<char> elevadores = new List<char>();
        List<char> turnos = new List<char>();

        public void answer(List<Data> ans)
        {
            answers = ans;
            for (int i = 0; i < answers.Count; i++)
            {
                andares.Add(answers[i].andar);
                elevadores.Add(answers[i].elevador);
                turnos.Add(answers[i].turno);
            }
        }

        /// <summary> Deve retornar uma List contendo o(s) andar(es) menos utilizado(s). </summary> 
        public List<int> andarMenosUtilizado()
        {
            List<int> respostas = andares;
            List<int> menosUsadosAndares = new List<int>();
            int[] _andares = new int[respostas.Max() + 1];
            int menor;

            // Conta quantas vezes foram respondidos | Mais usados.
            foreach (int andar in respostas)
            {
                _andares[andar]++;
            }

            // Achar o menos usado.
            menor = _andares[0];
            foreach (int usagem in _andares)
            {
                if (usagem < menor)
                    menor = usagem;
            }

            // Junta todos com o mesmo uso na lista.
            for (int i = 0; i < _andares.Length; i++)
            {
                if (_andares[i] == menor)
                    menosUsadosAndares.Add(i);
            }

            return menosUsadosAndares;
        }

        /// <summary> Deve retornar uma List contendo o(s) elevador(es) mais frequentado(s). </summary> 
        public List<char> elevadorMaisFrequentado()
        {
            List<char> respostas = elevadores;
            List<char> elevadorMaisUsado = new List<char>();

            Dictionary<char, int> elevators = new Dictionary<char, int>();

            foreach (char elevator in respostas)
            {
                if (!(elevators.ContainsKey(elevator)))
                {
                    elevators.Add(elevator, respostas.Count(n => n == elevator));
                }
            }

            foreach (char n in elevators.Keys)
            {
                if (elevators[n] == elevators.Values.Max())
                    elevadorMaisUsado.Add(n);
            }

            return elevadorMaisUsado;
        }

        /// <summary> Deve retornar uma List contendo o(s) elevador(es) menos frequentado(s). </summary> 
        public List<char> elevadorMenosFrequentado()
        {
            List<char> respostas = elevadores;
            List<char> elevadorMenosUsado = new List<char>();

            Dictionary<char, int> elevators = new Dictionary<char, int>();

            foreach (char elevator in respostas)
            {
                if (!(elevators.ContainsKey(elevator)))
                {
                    elevators.Add(elevator, respostas.Count(n => n == elevator));
                }
            }

            foreach (char n in elevators.Keys)
            {
                if (elevators[n] == elevators.Values.Min())
                    elevadorMenosUsado.Add(n);
            }

            return elevadorMenosUsado;
        }

        /// <summary> Deve retornar uma List contendo o período de maior fluxo de cada um dos elevadores mais frequentados (se houver mais de um). </summary> 
        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            List<char> usoElevadores = elevadorMaisFrequentado();
            List<char> elevMaior = new List<char>();
            List<char> nElements = new List<char>();
            int counter = 0;

            foreach (char elev in usoElevadores)
            {
                foreach (Data d in answers)
                {
                    if (d.elevador == elev)
                    {
                        nElements.Add(d.turno);
                    }
                }

                // Acha o maior.
                elevMaior.Add(' ');
                foreach (char turn in turnos.Distinct().ToList())
                {
                    if ((nElements.Count(n => n == turn)) > nElements.Count(m => m == elevMaior[counter]))
                    {
                        elevMaior[counter] = turn;
                    }
                }

                // Acha iguais.
                foreach (char turn in turnos.Distinct().ToList())
                {
                    if ((nElements.Count(n => n == turn)) == nElements.Count(m => m == elevMaior[counter]))
                    {
                        if (elevMaior[counter] == ' ')
                            elevMaior[counter] = turn;
                        else
                        {
                            elevMaior.Add(turn);
                            counter++;
                        }
                    }
                }

                counter++;
            }
            return elevMaior;
        }

        /// <summary> Deve retornar uma List contendo o período de menor fluxo de cada um dos elevadores menos frequentados (se houver mais de um). </summary> 
        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            List<char> usoElevadores = elevadorMaisFrequentado();
            List<char> nElements = new List<char>();
            List<char> elevMenor = new List<char>();
            int counter = 0;

            foreach (char elev in usoElevadores)

            {
                foreach (Data d in answers)
                {
                    if (d.elevador == elev)
                    {
                        nElements.Add(d.turno);
                    }
                }

                // Acha o maior.
                elevMenor.Add(' ');
                foreach (char turn in turnos.Distinct().ToList())
                {
                    if (elevMenor[counter] == ' ')
                        elevMenor[counter] = turn;

                    if ((nElements.Count(n => n == turn)) <= (nElements.Count(m => m == elevMenor[counter])))
                    {
                        elevMenor[counter] = turn;
                    }
                }

                // Acha iguais.
                foreach (char turn in turnos.Distinct().ToList())
                {
                    if ((nElements.Count(n => n == turn)) == nElements.Count(m => m == elevMenor[counter]))
                    {
                        elevMenor.Add(turn);
                        counter++;
                    }
                }

                counter++;
            }

            return elevMenor;
        }

        /// <summary> Deve retornar uma List contendo o(s) periodo(s) de maior utilização do conjunto de elevadores. </summary> 
        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            List<char> periodoMaior = new List<char>();
            List<char> turns = turnos;

            int maior = 0;
            foreach (char turn in turns)
            {
                if (turns.Count(n => n == turn) > maior)
                {
                    maior = turns.Count(n => n == turn);
                }
            }
            
            foreach (char turn in turns)
            {
                if (turns.Count(n => n == turn) == maior)
                {
                    if(!(periodoMaior.Contains(turn)))  
                        periodoMaior.Add(turn);
                }
            }
            return periodoMaior;
        }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador A em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorA() { return percentualDeUso('A'); }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador B em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorB() { return percentualDeUso('B'); }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador C em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorC() { return percentualDeUso('C'); }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador D em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorD() { return percentualDeUso('D'); }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador E em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorE() { return percentualDeUso('E'); }

        float percentualDeUso(char elevador)
        {
            float percent = 0;

            foreach (char elev in elevadores)
            {
                if (elev == elevador) 
                    percent++;
            }

            percent = (float)Math.Round(((percent * 100) / elevadores.Count), 2);

            return percent;
        }
    }
}
