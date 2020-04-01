using core;


namespace view
{
    /// <summary>
    /// Interface implemented by those who want to receive
    /// click events from the map.
    /// </summary>
    public interface MapObserver
    {
        /// <summary>
        /// This method is called when the user clicks on a specific
        /// country
        /// </summary>
        /// <param name="country">
        /// The country info specific to the country 
        /// the user clicked
        /// </param>
        void OnClick(CountryInfoEx country);
    }
}
