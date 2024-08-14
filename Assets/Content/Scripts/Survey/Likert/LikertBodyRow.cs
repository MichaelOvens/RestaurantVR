using UnityEngine;

namespace Survey.Likert
{
    public class LikertBodyRow : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private LikertQuestion _question;
        [SerializeField] private LikertResponses _responses;

        private LikertQuestionData _questionData;

        public void Populate(LikertQuestionData questionData, LikertResponseData responseData)
        {
            _questionData = questionData;

            _question.Populate(questionData.Text);
            _responses.Populate(responseData);

            _responses.OnSelectedResponseChanged += OnSelectedResponseChanged;
        }

        private void OnSelectedResponseChanged(object sender, int selectedResponse)
        {
            _questionData.SelectedResponse = selectedResponse;
        }

        public void UpdateLayout()
        {
            _rectTransform.sizeDelta = new Vector2()
            {
                x = _rectTransform.sizeDelta.x,
                y = _question.PreferredHeight
            };
        }
    }
}