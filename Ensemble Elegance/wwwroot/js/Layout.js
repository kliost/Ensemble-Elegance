const navLinks = document.querySelectorAll('.nav-links li a');
navLinks.forEach(link => {
    link.addEventListener('click', (event) => {
        navLinks.forEach(link => link.classList.remove('active'));
        event.target.classList.add('active');
    });
});

