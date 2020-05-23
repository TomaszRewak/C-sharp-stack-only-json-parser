foreach($algorithm in 'Newtonsoft', 'System.Text.Json', 'Utf8Json', 'Stack', 'StackOnly')
{
    foreach($elements in 1, 100, 1000, 10000, 100000, 200000, 500000, 1000000, 1500000, 2500000)
    {
        dotnet.exe "bin\Release\net5.0\StackOnlyJsonParser.PerformanceTests.dll" $algorithm $elements
    }
}