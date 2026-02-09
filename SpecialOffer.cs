//==========================================================
// Student Number : S10275337C
// Student Name : Dayana Sharafeena
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
//==========================================================

using System;

namespace PRGAssignment
{
    public class SpecialOffer
    {
        // ===== attributes =====
        private string restaurantId;
        private string offerCode;
        private string description;
        private double discountAmount;

        // ===== constructor =====
        public SpecialOffer(string restaurantId, string offerCode, string description, double discountAmount)
        {
            this.restaurantId = restaurantId;
            this.offerCode = offerCode;
            this.description = description;
            this.discountAmount = discountAmount;
        }

        // ===== properties =====
        public string RestaurantId
        {
            get { return restaurantId; }
        }

        public string OfferCode
        {
            get { return offerCode; }
        }

        public string Description
        {
            get { return description; }
        }

        public double DiscountAmount
        {
            get { return discountAmount; }
        }

        public override string ToString()
        {
            return $"{offerCode} - {description} (${discountAmount:0.00} off)";
        }
    }
}
