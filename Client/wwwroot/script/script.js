function setHeroImageHeight() {
    const heroImage = document.querySelector('.hero-image');
    const optionContainer = document.querySelector('.option-container');

    if (heroImage && optionContainer) {
        heroImage.style.height = `${optionContainer.offsetHeight}px`;
    }
}

window.addEventListener('resize', setHeroImageHeight);


function toggleNavMenu() {
    const navMenu = document.querySelector('.nav-menu');
    navMenu.style.display = (navMenu.style.display === 'none') ? 'block' : 'none';
}

function toggleSidebar() {
    const bodyContainer = document.querySelector('.body-container');
    bodyContainer.classList.toggle('sidebar-visible');
}