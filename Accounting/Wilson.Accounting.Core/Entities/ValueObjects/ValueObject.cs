using Newtonsoft.Json;

namespace Wilson.Accounting.Core.Entities.ValueObjects
{
    public abstract class ValueObject<TEntity> where TEntity : ValueObject<TEntity>
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as TEntity;

            if (ReferenceEquals(valueObject, null))
                return false;

            return EqualsCore(valueObject);
        }


        protected abstract bool EqualsCore(TEntity other);


        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }


        protected abstract int GetHashCodeCore();


        public static bool operator ==(ValueObject<TEntity> a, ValueObject<TEntity> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }


        public static bool operator !=(ValueObject<TEntity> a, ValueObject<TEntity> b)
        {
            return !(a == b);
        }

        public static explicit operator ValueObject<TEntity>(string json)
        {
            return JsonConvert.DeserializeObject<TEntity>(json);            
        }

        public static implicit operator string(ValueObject<TEntity> obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
