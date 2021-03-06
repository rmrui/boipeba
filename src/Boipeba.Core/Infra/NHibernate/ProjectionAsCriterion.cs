﻿#pragma warning disable 1591
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Engine;
using NHibernate.SqlCommand;

namespace Boipeba.Core.Infra.NHibernate
{
    public class ProjectionAsCriterion : AbstractCriterion
    {
        private readonly IProjection _projection;

        public ProjectionAsCriterion(IProjection projection)
        {
            _projection = projection;
        }

        public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery,
            IDictionary<string, IFilter> enabledFilters)
        {
            var columnNames = CriterionUtil.GetColumnNamesForSimpleExpression(
                null, _projection, criteriaQuery, criteria, enabledFilters, this, string.Empty);

            var sqlBuilder = new SqlStringBuilder(4 * columnNames.Length);

            for (int i = 0; i < columnNames.Length; i++)
            {
                if (i > 0)
                {
                    sqlBuilder.Add(" and ");
                }

                sqlBuilder.Add(columnNames[i]);
            }
            return sqlBuilder.ToSqlString();
        }

        public override TypedValue[] GetTypedValues(ICriteria criteria, ICriteriaQuery criteriaQuery)
        {
            var typedValues = new List<TypedValue>();

            if (_projection != null)
            {
                typedValues.AddRange(_projection.GetTypedValues(criteria, criteriaQuery));
            }
            typedValues.Add(GetParameterTypedValue(criteria, criteriaQuery));

            return typedValues.ToArray();
        }

        private TypedValue GetParameterTypedValue(ICriteria criteria, ICriteriaQuery criteriaQuery)
        {
            return CriterionUtil.GetTypedValues(criteriaQuery, criteria, _projection, null).Single();
        }

        public override IProjection[] GetProjections()
        {
            return new[] { _projection };
        }

        public override string ToString()
        {
            return _projection.ToString();
        }
    }
}
