﻿using Lucinq.Core.Querying;

namespace Lucinq.Visitors
{
    public interface IQueryBuilderVisitor
    {
        void VisitQueryBuilder(ICoreQueryBuilder queryBuilder);
    }
}
