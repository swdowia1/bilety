using System;

namespace Loty
{
    // Base class for Tenant
    public abstract class Tenant
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        protected Tenant(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public abstract bool CanLogDiscountCriteria();
    }
    public class TenantA : Tenant
    {
        public TenantA(string name) : base(name) { }
        public override bool CanLogDiscountCriteria() => true;
    }

    public class TenantB : Tenant
    {
        public TenantB(string name) : base(name) { }
        public override bool CanLogDiscountCriteria() => false;
    }


}
