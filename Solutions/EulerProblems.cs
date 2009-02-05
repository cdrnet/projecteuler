using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MathNet.Numerics;
using ProjectEuler.Solutions.Algorithms;

namespace ProjectEuler.Solutions
{
    public class EulerProblems
    {
        List<Problem> _problems;
        Dictionary<int, Problem> _promblemById;
        Stopwatch _watch;

        public EulerProblems()
        {
            _problems = new List<Problem>
            {
                new Problem(1, "What is the sum of all the multiples of 3 or 5 below 1000?")
                {
                    new Approach
                    {
                        Title = "Bruteforce",
                        WarmupRounds = 5,
                        BenchmarkRounds = 1000,
                        Algorithm = delegate
                        {
                            int sum = 0;
                            for (int i = 1; i < 1000; i++)
                            {
                                if (i.IsMultiple(3) || i.IsMultiple(5))
                                    sum += i;
                            }
                            return sum;
                        }
                    },

                    new Approach
                    {
                        Title = "Explicit",
                        WarmupRounds = 5,
                        BenchmarkRounds = 1000,
                        Algorithm = delegate
                        {
                            // Problem Parameters
                            int n = 1000;
                            int a = 3;
                            int b = 5;

                            n--; // "below"
                            long lcm = Fn.Lcm(a, b);
                            long suma = Series.SumOfEveryKthIntegers(a, n);
                            long sumb = Series.SumOfEveryKthIntegers(b, n);
                            long sumlcm = Series.SumOfEveryKthIntegers(lcm, n);
                            return suma + sumb - sumlcm;
                        }
                    }
                },

                new Problem(2, "What is the sum of all the even-valued terms in the sequence which do not exceed four million?")
                {
                    new Approach
                    {
                        Title = "Bruteforce 1",
                        WarmupRounds = 5,
                        BenchmarkRounds = 1000,
                        Algorithm = delegate
                        {
                            double[] fibSeq = Solution.GetFibonacciSequence(4000000);
                            double sum = 0;

                            foreach (double term in fibSeq)
                            {
                                if (term % 2 == 0)
                                    sum += term;
                            }

                            return Convert.ToInt64(sum);
                        }
                    },

                    new Approach
                    {
                        Title = "Bruteforce 2",
                        WarmupRounds = 5,
                        BenchmarkRounds = 1000,
                        Algorithm = () => Sequence.EnumerateFibonacciEven(4000000).Sum()
                    }
                },

                new Problem(3, "What is the largest Prime factor of the number 600851475143?")
                {
                    new Approach
                    {
                        Algorithm = delegate
                        {
                            double[] primeFactors = 600851475143d.getPrimeFactors();
                            return Convert.ToInt64(primeFactors.Max());     
                        }
                    }
                },

                new Problem(4, "What is the largest palindrome made from the produce of two 3-digit numbers?")
                {
                    new Approach
                    {
                        Algorithm = delegate
                        {
                            int max = 0;
                            int term = 0;

                            for (int i = 100; i < 1000; i++)
                            {
                                for (int j = 100; j < 1000; j++)
                                {
                                    term = i * j;
                                    if (term.IsPalindrom())
                                    {
                                        if (term > max)
                                            max = term;
                                    }
                                }
                            }
                            return max;
                        }
                    }
                },

                new Problem(5, "What is the smallest number that is evenly divisible by all of the numbers from 1 to 20?")
                {
                    new Approach
                    {
                        WarmupRounds = 1,
                        BenchmarkRounds = 1,
                        Algorithm = delegate
                        {
                            double answer = -1;
                            for (double i = 2520; i < 400000000; i++)
                            {
                                for (double j = 2; j < 21; j++)
                                {
                                    if (i % j != 0)
                                        break;

                                    if (j == 20)
                                        answer = i;
                                }

                                if (answer > 0)
                                    break;
                            }
                            return Convert.ToInt64(answer);
                        }
                    }
                },

                new Problem(6, "What is the difference between the sum of the squares and the square of the sums for the first one hundred natural numbers?")
                {
                    new Approach
                    {
                        Algorithm = delegate
                        {
                            double sumOfSquares = 100.SumOfSquares();
                            double squareOfSums = 100.SquareOfSums();
                            return Convert.ToInt64(squareOfSums - sumOfSquares);
                        }
                    },

                    new Approach
                    {
                        Algorithm = delegate
                        {
                            //I'm sure this is faster because you only loop through the terms once
                            //instead of twice.
                            double sum_squares = 0;
                            double square_sums = 0;
                            for (int i = 1; i <= 100; i++)
                            {
                                sum_squares += i * i;
                                square_sums += i;
                            }
                            square_sums *= square_sums;

                            return Convert.ToInt64(square_sums - sum_squares);
                        }
                    }
                },

                new Problem(7, "What is the 10,001st prime number?")
                {
                    new Approach
                    {
                        Algorithm = delegate
                        {
                            int count = 0;
                            double number = 1;
                            while (count < 10001)
                            {
                                number++;
                                if (number.IsPrime())
                                    count++;
                            }
                            return Convert.ToInt64(number);
                        }
                    }
                },

                new Problem(8, "What is greatest product of five consecutive digits in the 1000 digit number?")
                {
                    new Approach
                    {
                        Algorithm = delegate
                        {
                            string number = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";
                            double product = 0;
                            double temp = 1;
                            int count = -1;

                            int[] consecutiveValues = new int[5];
                            consecutiveValues[0] = Convert.ToInt32(number[0].ToString());
                            consecutiveValues[1] = Convert.ToInt32(number[1].ToString());
                            consecutiveValues[2] = Convert.ToInt32(number[2].ToString());
                            consecutiveValues[3] = Convert.ToInt32(number[3].ToString());
                            consecutiveValues[4] = Convert.ToInt32(number[4].ToString());

                            for (int i = 4; i < number.Length; i++)
                            {
                                if (count >= 0)
                                    consecutiveValues[count % 5] = Convert.ToInt32(number[i].ToString());

                                foreach (int num in consecutiveValues)
                                    temp *= num;
                                if (temp > product)
                                    product = temp;

                                count++;
                                temp = 1;
                            }

                            return Convert.ToInt64(product);
                        }
                    }
                },

                new Problem(9, "Find the product of abc where: a<b<c, a^2 + b^2 = c^2, and a + b + c = 1000.")
                {
                    new Approach
                    {
                        Algorithm = Solution.Problem9
                    }
                },

                new Problem(10, "What is the sum of all the primes below two million?")
                {
                    new Approach
                    {
                        Algorithm = delegate
                        {
                            double sum = 2;
                            for (double i = 3; i < 2000000; i += 2)
                            {
                                if (i.IsPrime())
                                    sum += i;
                            }
                            return Convert.ToInt64(sum);
                        }
                    },

                    new Approach
                    {
                        Algorithm = delegate
                        {
                            List<bool> list = new List<bool>();
                            for (int i = 0; i < 2000000; i++)
                                list.Add(true);
                            list[0] = false;
                            list[1] = false;
                            list = Solution.EratosthenesSieve(list, 2);

                            double sum = 0;
                            for (int i = 0; i < 2000000; i++)
                            {
                                if (list[i])
                                    sum += i;
                            }
                            return Convert.ToInt64(sum);
                        }
                    }
                },

                new Problem(11, "What is the greatest product of four adjacent numbers in any direction in the 20x20 grid?")
                {
                    new Approach
                    {
                        Algorithm = Solution.Problem11
                    }
                },

                new Problem(12, "What is the first triangle number to have over 500 divisors?")
                {
                    new Approach
                    {
                        Algorithm = delegate
                        {
                            double term = 0;
                            double triangleNumber = 0;

                            do
                            {
                                term++;
                                triangleNumber = term.TriangleNumber();
                            } while (triangleNumber.NumberOfDivisors() < 500);

                            return Convert.ToInt64(triangleNumber);
                        }
                    }
                },

                new Problem(13, "What are the first 10 digits of the sum of the 100 50-digit numbers?")
                {
                    new Approach
                    {
                        Algorithm = delegate
                        {
                            string number = "37107287533902102798797998220837590246510135740250463769376774900097126481248969700780504170182605387432498619952474105947423330951305812372661730962991942213363574161572522430563301811072406154908250230675882075393461711719803104210475137780632466768926167069662363382013637841838368417873436172675728112879812849979408065481931592621691275889832738442742289174325203219235894228767964876702721893184745144573600130643909116721685684458871160315327670386486105843025439939619828917593665686757934951621764571418565606295021572231965867550793241933316490635246274190492910143244581382266334794475817892575867718337217661963751590579239728245598838407582035653253593990084026335689488301894586282278288018119938482628201427819413994056758715117009439035398664372827112653829987240784473053190104293586865155060062958648615320752733719591914205172558297169388870771546649911559348760353292171497005693854370070576826684624621495650076471787294438377604532826541087568284431911906346940378552177792951453612327252500029607107508256381565671088525835072145876576172410976447339110607218265236877223636045174237069058518606604482076212098132878607339694128114266041808683061932846081119106155694051268969251934325451728388641918047049293215058642563049483624672216484350762017279180399446930047329563406911573244438690812579451408905770622942919710792820955037687525678773091862540744969844508330393682126183363848253301546861961243487676812975343759465158038628759287849020152168555482871720121925776695478182833757993103614740356856449095527097864797581167263201004368978425535399209318374414978068609844840309812907779179908821879532736447567559084803087086987551392711854517078544161852424320693150332599594068957565367821070749269665376763262354472106979395067965269474259770973916669376304263398708541052684708299085211399427365734116182760315001271653786073615010808570091499395125570281987460043753582903531743471732693212357815498262974255273730794953759765105305946966067683156574377167401875275889028025717332296191766687138199318110487701902712526768027607800301367868099252546340106163286652636270218540497705585629946580636237993140746255962240744869082311749777923654662572469233228109171419143028819710328859780666976089293863828502533340334413065578016127815921815005561868836468420090470230530811728164304876237919698424872550366387845831148769693215490281042402013833512446218144177347063783299490636259666498587618221225225512486764533677201869716985443124195724099139590089523100588229554825530026352078153229679624948164195386821877476085327132285723110424803456124867697064507995236377742425354112916842768655389262050249103265729672370191327572567528565324825826546309220705859652229798860272258331913126375147341994889534765745501184957014548792889848568277260777137214037988797153829820378303147352772158034814451349137322665138134829543829199918180278916522431027392251122869539409579530664052326325380441000596549391598795936352974615218550237130764225512118369380358038858490341698116222072977186158236678424689157993532961922624679571944012690438771072750481023908955235974572318970677254791506150550495392297953090112996751986188088225875314529584099251203829009407770775672113067397083047244838165338735023408456470580773088295917476714036319800818712901187549131054712658197623331044818386269515456334926366572897563400500428462801835170705278318394258821455212272512503275512160354698120058176216521282765275169129689778932238195734329339946437501907836945765883352399886755061649651847751807381688378610915273579297013376217784275219262340194239963916804498399317331273132924185707147349566916674687634660915035914677504995186714302352196288948901024233251169136196266227326746080059154747183079839286853520694694454072476841822524674417161514036427982273348055556214818971426179103425986472045168939894221798260880768528778364618279934631376775430780936333301898264209010848802521674670883215120185883543223812876952786713296124747824645386369930090493103636197638780396218407357239979422340623539380833965132740801111666627891981488087797941876876144230030984490851411606618262936828367647447792391803351109890697907148578694408955299065364044742557608365997664579509666024396409905389607120198219976047599490197230297649139826800329731560371200413779037855660850892521673093931987275027546890690370753941304265231501194809377245048795150954100921645863754710598436791786391670211874924319957006419179697775990283006991536871371193661495281130587638027841075444973307840789923115535562561142322423255033685442488917353448899115014406480203690680639606723221932041495354150312888033953605329934036800697771065056663195481234880673210146739058568557934581403627822703280826165707739483275922328459417065250945123252306082291880205877731971983945018088807242966198081119777158542502016545090413245809786882778948721859617721078384350691861554356628840622574736922845095162084960398013400172393067166682355524525280460972253503534226472524250874054075591789781264330331690";
                            double sum = 0;
                            int digitLenth = 50;

                            for (int i = 0; i < number.Length; i += digitLenth)
                            {
                                double t = Convert.ToDouble(number.Substring(i, digitLenth - 1).ToString());
                                sum += Convert.ToDouble(number.Substring(i, digitLenth - 1).ToString());
                            }
                            
                            return Convert.ToInt64(sum);
                        }
                    }
                },

                new Problem(14, "?")
                {
                    new Approach
                    {
                        Algorithm = Solution.Problem14
                    }
                },

                new Problem(16, "What is the sum of the digits of the number 2^1000?")
            };

            _promblemById = _problems.ToDictionary(p => p.ID);
            _watch = new Stopwatch();
        }

        public void RunProblem(int id)
        {
            Problem problem;
            if(!_promblemById.TryGetValue(id, out problem))
            {
                Console.WriteLine("Problem {0} is not available.", id);
                return;
            }

            problem.Run(_watch);
        }
    }
}
