using UnityEngine;
using UnityEngine.UI;

public class StarSizeAdjuster : MonoBehaviour
{
    [SerializeField] private GameObject starContainer; // The inner Horizontal Layout Group containing the stars
    [SerializeField] private int numberOfStars; // Number of stars to display
    [SerializeField] private GameObject starPrefab; // Prefab of the star image

    public void AdjustStarSizes()
    {
        // Assume each star has the same size and there's no spacing between stars for simplicity
        float totalAvailableWidth = starContainer.GetComponent<RectTransform>().rect.width;
        float spacing = starContainer.GetComponent<HorizontalLayoutGroup>().spacing;
        float totalSpacing = spacing * (numberOfStars - 1); // Total spacing between stars
        float widthPerStar = (totalAvailableWidth - totalSpacing) / numberOfStars;

        // Clear previous stars
        foreach (Transform child in starContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Generate new stars and adjust their sizes
        for (int i = 0; i < numberOfStars; i++)
        {
            GameObject star = Instantiate(starPrefab, starContainer.transform);
            RectTransform starRect = star.GetComponent<RectTransform>();

            // Set the size of the star
            starRect.sizeDelta = new Vector2(widthPerStar, widthPerStar);
        }
    }

    // Call this method to update stars based on difficulty
    public void UpdateStarsBasedOnDifficulty(int difficulty)
    {
        numberOfStars = difficulty; // Set the number of stars based on difficulty
        AdjustStarSizes(); // Adjust the sizes
    }
}