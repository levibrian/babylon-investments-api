using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Shared.Extensions
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> And<T>(
            this ISpecification<T> firstSpecification,
            ISpecification<T> secondSpecification)
        {
            return new AndSpecification<T>(firstSpecification, secondSpecification);
        }
        
        public static IResultedSpecification<T> And<T>(
            this IResultedSpecification<T> firstSpecification,
            IResultedSpecification<T> secondSpecification)
        {
            return new AndSpecification<T>(firstSpecification, secondSpecification);
        }
        
        public static ISpecification<T> Or<T>(this ISpecification<T> firstSpecification,
            ISpecification<T> secondSpecification)
        {
            return new OrSpecification<T>(firstSpecification, secondSpecification);
        }
        
        public static IResultedSpecification<T> Or<T>(this IResultedSpecification<T> firstSpecification,
            IResultedSpecification<T> secondSpecification)
        {
            return new OrSpecification<T>(firstSpecification, secondSpecification);
        } 
    }
}