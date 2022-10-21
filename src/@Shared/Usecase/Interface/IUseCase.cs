using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Shared.Usecase.Interface
{
    public interface IUseCase<T1,T2>
    {
        Task<T2> Execute(T1 input);
    }
}
