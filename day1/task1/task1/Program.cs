using L2O___D09;

namespace task1;

class Program
{
    static void Main(string[] args)
    {


        var pList = ListGenerators.ProductList;
        var cList = ListGenerators.CustomerList;
        var words = File.ReadAllLines("dictionary_english.txt");

        //Restriction Operators
        var outOfStockProducts = pList.Where(p => p.UnitsInStock == 0);

        printList(outOfStockProducts);

        var inStockAndCostMoreThan3 = pList.Where(p => p.UnitsInStock >= 1 && p.UnitPrice > 3);
        printList(inStockAndCostMoreThan3);

        string[] Arr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        var digits = Arr.Where((num, index) =>
        {
            return num.Length < index;
        });

        printList(digits);

        //LINQ - Element Operators
        var firstOutOfStock = pList.First(p => p.UnitsInStock <= 0);
        Console.WriteLine(firstOutOfStock);

        var firstP = pList.FirstOrDefault(p => p.UnitPrice > 1000);
        if (firstP == null)
        {
            Console.WriteLine("there is no product that meets this condtion");
        }
        Console.WriteLine(firstP);
        int[] Arr2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

        var secondNum = Arr2.Where(p => p > 5).ElementAt(1);
        Console.WriteLine(secondNum);



        //LINQ - Set Operators
        //1
        var uniqueByCategory = pList.DistinctBy(p => p.Category).Select(p => p.Category);
        printList(uniqueByCategory);

        //2
        var uniqueFirstLettersProducts = pList.DistinctBy(p => p.ProductName[0]).Select(p => p.ProductName[0]);
        printList(uniqueFirstLettersProducts);
        var uniqueFirstLettersCustomer = cList.DistinctBy(c => c.CompanyName[0]).Select(c => c.CompanyName[0]);
        printList(uniqueFirstLettersCustomer);


        var CommonFirstLetter = uniqueFirstLettersCustomer.Union(uniqueFirstLettersProducts);
        printList(CommonFirstLetter);

        var firstProductLetterNotCustomer = uniqueFirstLettersProducts.Except(uniqueFirstLettersCustomer);
        printList(firstProductLetterNotCustomer);


        //5
        var lastThree = pList.Select(p => p.ProductName.Substring(0, 3))
        .Concat(cList.Select(c => c.CompanyName.Substring(0, 3)));

        printList(lastThree);

        Console.WriteLine("-----");

        //LINQ Aggregate Operators
        //1
        int[] ArrCount = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
        var oddNums = ArrCount.Count(a => a % 2 != 1);
        Console.WriteLine(oddNums);

        //2
        var orderCount = cList.Select(c => c.Orders.Count());
        Console.WriteLine(orderCount);
        printList(orderCount);

        //3
        // var categoiresCount =pList.Select(new{p=>p.Category});
        var catCount = pList.GroupBy(p => p.Category).Select(p => new { Category = p.Key, Count = p.Count() });
        printList(catCount);

        //4
        int[] getTotal = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
        var total = getTotal.Sum();
        Console.WriteLine(total);

        //5
        var numOfCharachter = words.Select(w => w.Length);
        printList(numOfCharachter);

        //6
        var totalInStock = pList.GroupBy(p => p.Category).Select(p => new
        {
            Cat = p.Key,
            tInStock = p.Sum(p => p.UnitsInStock)
        });
        printList(totalInStock);
        //7

        var ShortestWord = Enumerable.Min(numOfCharachter);
        Console.WriteLine(ShortestWord);

        //8
        var cheapestPrice = pList.GroupBy(p => p.Category).Select(c => new
        {
            Cat = c.Key,
            cheapestPrice = c.Min(p => p.UnitPrice)
        });
        printList(cheapestPrice);

        //9 ----??
        var cheapestProducts = pList.GroupBy(p => p.Category)
        .Select(c =>
        new
        {
            cat = c.Key,
            cheapestPord = c.Where(p => p.UnitPrice == c.Min(c => c.UnitPrice)).Select(p => new { p.ProductName, p.UnitPrice }).FirstOrDefault()
        });
        var cheapestProductsUsingLet = from p in pList
                                       group p by p.Category into g
                                       let minPrice = g.Min(p => p.UnitPrice) // Calculate minimum price in each category
                                       from p in g
                                       where p.UnitPrice == minPrice
                                       select new
                                       {
                                           p.Category,
                                           p.ProductName,
                                           p.UnitPrice
                                       };

        printList(cheapestProducts);
        Console.WriteLine("-----");
        printList(cheapestProductsUsingLet);

        //10
        var longestWord = words.Max(w=>w.Length);
        Console.WriteLine(longestWord);


        //11
        var maxPriceForCat = pList.GroupBy(p => p.Category).Select(c => new
        {
            Cat = c.Key,
            maxPrice = c.Max(p => p.UnitPrice)
        });
        printList(maxPriceForCat);

        //12
        var priceiestProducts = pList.GroupBy(p => p.Category)
        .Select(c =>
        new
        {
            cat = c.Key,
            priceiestPord = c.Where(p => p.UnitPrice == c.Max(c => c.UnitPrice)).Select(p => new { p.ProductName, p.UnitPrice }).FirstOrDefault()
        });

        printList(priceiestProducts);

        //13

        var averageL = words.Average(w=>w.Length);
        Console.WriteLine(averageL);

        //14
        var avPriceForCat = pList.GroupBy(p => p.Category).Select(c => new
        {
            Cat = c.Key,
            avPrice = c.Average(p => p.UnitPrice)
        });
        printList(avPriceForCat);


//LINQ - Ordering Operators
//
    //1
    var orderedByname = pList.OrderBy(p=>p.ProductName).Select(p=>p.ProductName);
    printList(orderedByname);
    //2
    string[] StringArray = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
    var orderInsensitive = StringArray.OrderBy(w=>w.ToLower());
        printList(orderInsensitive);

    //3
    var productsOrderedByStock = pList.OrderByDescending(p=>p.UnitsInStock).Select(p=>new{p.ProductName,p.UnitsInStock});
    printList(productsOrderedByStock);
    //4
    string[] NumberString = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight","nine" };
    var sortByLenAndName= NumberString.OrderBy(w=>w.Length).ThenBy(w=>w);
    printList(sortByLenAndName);
    //5
    var sortByLenAndCaseName= StringArray.OrderBy(w=>w.Length).ThenBy(w=>w.ToLower());
    printList(sortByLenAndCaseName);
    //6
    var sortedCatThenPriceDesc = pList.OrderBy(p=>p.Category).ThenByDescending(p=>p.UnitPrice).Select(p=>new{p.Category,p.UnitPrice,p.ProductName});
    printList(sortedCatThenPriceDesc);

    //7
    var sortByLenAndCaseNameDesc= StringArray.OrderBy(w=>w.Length).ThenByDescending(w=>w.ToLower());
    printList(sortByLenAndCaseName);

    //8
    var solution = NumberString.Where(w=>w[1]=='i').Reverse();
    printList(solution);

    //LINQ - Partitioning Operators
    //1
    var ordersFromW = cList.Where(c=>c.Region=="WA").Select(c=>string.Join(" ||",c.Orders.Take(3)));
    printList(ordersFromW);

    // var firstThree = cList.
    //2
    var allbutFirstTwo =cList.Where(c=>c.Region=="WA").Select(c=>string.Join(" ||",c.Orders.Skip(2)));
    printList(allbutFirstTwo);
    //3
    int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
    var untilNumisLess = numbers.TakeWhile((n,i)=>n>=i);
    printList(untilNumisLess);

    //4
    var startingByD3= numbers.SkipWhile(n=>n%3!=0);
    printList(startingByD3);
    //5
    var startingFromLess = numbers.SkipWhile((n,i)=>n>=i);
    printList(startingFromLess);


    //LINQ - Projection Operators
    //1
    var justTheNames = pList.Select(p=>p.ProductName);
    printList(justTheNames);
    //2
    string[] newWords = { "aPPLE", "BlUeBeRrY", "cHeRry" };
    var upperAndLower = newWords.Select(w=>new{upper=w.ToUpper(),lower=w.ToLower()});
    printList(upperAndLower);

    //3
    var someProps = pList.Select(p=>new{Price=p.UnitPrice,Name=p.ProductName});
        printList(someProps);

    //4    
    int[] numArr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
    var inPlace =   numArr.Select((n,i)=>{

        bool isInPlace = n==i;
        return $"{n}: {isInPlace}";

    });
    printList(inPlace);
    int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
    int[] numbersB = { 1, 3, 5, 7, 8 };


    //5
    var r = numbersA.SelectMany(a=>numbersB.Where(b=>b>a).Select(b=>$"{a} is less than {b}"));

    printList(r);

    //6
    var ordersLessThan500= cList.SelectMany(c=>c.Orders.Where(o=>o.Total<500));
    printList(ordersLessThan500);

//7
    var orderByDate= cList.SelectMany(c=>c.Orders.Where(o=>o.OrderDate>new DateTime(1998,1,1)));
    printList(orderByDate);


  //LINQ - Quantifiers
  var   containsEi = words.Any(w=>w.Contains("ie"));
  Console.WriteLine(containsEi);

  //2
  var atleastOneOutOfStock = pList.GroupBy(p=>p.Category).Where(g=>g.Any(p=>p.UnitsInStock==0)).SelectMany(p=>p);
       printList(atleastOneOutOfStock);

//3
  var allInStock = pList.GroupBy(p=>p.Category).Where(g=>g.All(p=>p.UnitsInStock>0)).SelectMany(p=>p);
       printList(allInStock);

//LINQ - Grouping Operators
    int[] numbers2 = { 0, 5, 10, 1, 6, 11, 7, 2, 12, 3,8,13,4,9,14};
    //1-->console log
    var gByRemainder = numbers2.GroupBy(n=>n%5).OrderBy(g=>g.Key);
           
    //2
    var groupByFirstLetter = words.GroupBy((w)=>{
        foreach(char c in w){
            if(char.IsLetter(c)){
                return c;
            }
        }
        return w[0];
    });

    printList(groupByFirstLetter.Select(g=>new {g.Key,count=g.Count()}));

    //3
    string[] ArrWords = { "from ", " salt", " earn ", " last ", " near ","form "};
    var result = ArrWords.GroupBy((w)=>{
        string realString="";
        foreach(char c in w.OrderBy(c=>c)){
            if(char.IsLetter(c)){
                realString+=c;
            }
        }
    return realString;
    });

    foreach(var g in result){
        printList(g);
    }

    printList(result.Select(g=>new{g.Key,count=g.Count()}));
      
    }

    private static void printList(IEnumerable<int> o)
    {
        foreach (var r in o)
        {
            Console.Write(r);
            Console.Write("-");

        }
        Console.WriteLine("-");
    }

    private static void printList(IEnumerable<char> o)
    {
        foreach (var r in o)
        {
            Console.Write(r);
            Console.Write("-");

        }
        Console.WriteLine("-");

    }

    static void printList(IEnumerable<object> o)
    {
        foreach (var r in o)
        {
            Console.WriteLine(r);
        }

    }






}
