using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Delay
{
    public async static Task run(float time)
    {
        await Task.Delay(Convert.ToInt32(time * 1000));
        return;
    }
}