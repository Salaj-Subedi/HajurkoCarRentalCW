// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const cardsContainer = document.querySelector('.cards-container');
const cards = document.querySelectorAll('.card');
const prevBtn = document.querySelector('.prev-btn');
const nextBtn = document.querySelector('.next-btn');
const cardWidth = cards[0].offsetWidth;
const cardMarginRight = parseInt(window.getComputedStyle(cards[0]).marginRight);

let currentCardIndex = 0;
let cardTranslateX = 0;

cardsContainer.style.width = `${(cardWidth + cardMarginRight) * cards.length - cardMarginRight}px`;

function slideCards() {
    cardsContainer.style.transform = `translateX(-${cardTranslateX}px)`;
}

nextBtn.addEventListener('click', () => {
    if (currentCardIndex < cards.length - 1) {
        currentCardIndex++;
        cardTranslateX = currentCardIndex * (cardWidth + cardMarginRight);
        slideCards();
    }
});

prevBtn.addEventListener('click', () => {
    if (currentCardIndex > 0) {
        currentCardIndex--;
        cardTranslateX = currentCardIndex * (cardWidth + cardMarginRight);
        slideCards();
    }
});