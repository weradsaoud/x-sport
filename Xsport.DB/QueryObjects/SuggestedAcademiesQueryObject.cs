﻿using Xsport.Common.Enums;
using Xsport.DB.Entities;
using Xsport.DTOs.AcademyDtos;

namespace Xsport.DB.QueryObjects
{
    public static class SuggestedAcademiesQueryObject
    {
        public static IQueryable<SuggestedAcademyDto> MapAcademiesToSuggested(
            this IQueryable<Academy> academies, short currentLanguageId)
        {
            try
            {
                return academies.Select(academy => new SuggestedAcademyDto
                {
                    AcademyId = academy.AcademyId,
                    Name = academy.AcademyTranslations
                    .Single(t => t.LanguageId == currentLanguageId).Name,
                    Description = academy.AcademyTranslations
                    .Single(t => t.LanguageId == currentLanguageId).Description,
                    Lat = academy.Lattitude,
                    Long = academy.Longitude,
                    OpenTime = academy.OpenAt,
                    CloseTime = academy.CloseAt,
                    MinPrice = academy.Courses.OrderBy(c => c.Price).First().Price,
                    NumReviews = academy.AcademyReviews.Count,
                    Evaluation = academy.AcademyReviews.Average(r => r.Evaluation),
                    CoverPhoto = academy.Mutimedias.Single(m => m.IsCover && !m.IsVideo).FilePath,
                    CoverVideo = academy.Mutimedias.Single(m => m.IsCover && m.IsVideo).FilePath,
                    Photos = academy.Mutimedias
                    .Where(m => !m.IsVideo && !m.IsCover)
                    .Select(m => m.FilePath).ToList(),
                    Videos = academy.Mutimedias
                    .Where(m => m.IsVideo && !m.IsCover)
                    .Select(m => m.FilePath).ToList(),
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static IQueryable<SuggestedAcademyDto> OrderSuggestedAcademies(
            this IQueryable<SuggestedAcademyDto> suggestedAcademies,
            SuggestedAcademiesOrderOptions orderOptions)
        {
            try
            {
                switch (orderOptions)
                {
                    case SuggestedAcademiesOrderOptions.SimpleOrder:
                        return suggestedAcademies.OrderBy(s => s.AcademyId);
                    case SuggestedAcademiesOrderOptions.NameDesc:
                        return suggestedAcademies.OrderByDescending(s => s.Name);
                    case SuggestedAcademiesOrderOptions.NameAsc:
                        return suggestedAcademies.OrderBy(s => s.Name);
                    case SuggestedAcademiesOrderOptions.MinPriceUp:
                        return suggestedAcademies.OrderBy(s => s.MinPrice);
                    case SuggestedAcademiesOrderOptions.MinPriceDown:
                        return suggestedAcademies.OrderByDescending(s => s.MinPrice);
                    case SuggestedAcademiesOrderOptions.EvaluationUp:
                        return suggestedAcademies.OrderBy(s => s.Evaluation);
                    case SuggestedAcademiesOrderOptions.EvaluationDown:
                        return suggestedAcademies.OrderByDescending(s => s.Evaluation);
                    case SuggestedAcademiesOrderOptions.NumOfReviewsUp:
                        return suggestedAcademies.OrderBy(s => s.NumReviews);
                    case SuggestedAcademiesOrderOptions.NumOfReviewsDown:
                        return suggestedAcademies.OrderByDescending(s => s.NumReviews);
                    default:
                        throw new ArgumentOutOfRangeException(
                        nameof(orderOptions), orderOptions, null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static IQueryable<SuggestedAcademyDto> FilterSuggestedAcademies(
            this IQueryable<SuggestedAcademyDto> suggestedAcademies,
            SuggestedAcademiesFilterOptions filterOption, string filterValue)
        {
            try
            {
                if (string.IsNullOrEmpty(filterValue)) return suggestedAcademies;
                switch (filterOption)
                {
                    case SuggestedAcademiesFilterOptions.None: return suggestedAcademies;
                    case SuggestedAcademiesFilterOptions.ByName:
                        return suggestedAcademies.Where(s => s.Name.Contains(filterValue));
                    case SuggestedAcademiesFilterOptions.ByPriceUp:
                        decimal priceUp = decimal.Parse(filterValue);
                        return suggestedAcademies.Where(s => s.MinPrice >= priceUp);
                    case SuggestedAcademiesFilterOptions.ByPriceDown:
                        decimal priceDown = decimal.Parse(filterValue);
                        return suggestedAcademies.Where(s => s.MinPrice <= priceDown);
                    case SuggestedAcademiesFilterOptions.ByPrice:
                        decimal price = decimal.Parse(filterValue);
                        return suggestedAcademies.Where(s => s.MinPrice == price);
                    case SuggestedAcademiesFilterOptions.ByEvaluationDown:
                        double evaluationDown = double.Parse(filterValue);
                        return suggestedAcademies.Where(s => s.Evaluation <= evaluationDown);
                    case SuggestedAcademiesFilterOptions.ByEvaluationUp:
                        double evaluationUp = double.Parse(filterValue);
                        return suggestedAcademies.Where(s => s.Evaluation >= evaluationUp);
                    case SuggestedAcademiesFilterOptions.ByEvaluation:
                        double evaluation = double.Parse(filterValue);
                        return suggestedAcademies.Where(s => s.Evaluation == evaluation);
                    case SuggestedAcademiesFilterOptions.NumOfReviewsDown:
                        int numOfReviewsDown = int.Parse(filterValue);
                        return suggestedAcademies.Where(s => s.NumReviews <= numOfReviewsDown);
                    case SuggestedAcademiesFilterOptions.NumOfReviewsUp:
                        int numOfReviewsUp = int.Parse(filterValue);
                        return suggestedAcademies.Where(s => s.NumReviews >= numOfReviewsUp);
                    case SuggestedAcademiesFilterOptions.NumOfReviews:
                        int numOfReviews = int.Parse(filterValue);
                        return suggestedAcademies.Where(s => s.NumReviews == numOfReviews);
                    default:
                        throw new ArgumentOutOfRangeException
                        (nameof(filterOption), filterOption, null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
