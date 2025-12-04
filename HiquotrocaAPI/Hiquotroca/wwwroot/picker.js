window.pickerAttach = (element, dotnetRef) => {
    if (!element) return;
    const itemHeight = 56; // must match CSS .picker-row height
    let ticking = false;

    const update = () => {
        // center pos
        const center = element.scrollTop + (element.clientHeight / 2);
        // compute index precisely
        const index = Math.round((center - (itemHeight / 2)) / itemHeight);
        const max = Math.max(0, element.children.length - 1);
        const idx = Math.min(Math.max(0, index), max);
        if (dotnetRef) dotnetRef.invokeMethodAsync('SetCenterIndex', idx);
    };

    element.addEventListener('scroll', () => {
        if (!ticking) {
            window.requestAnimationFrame(() => { update(); ticking = false; });
            ticking = true;
        }
    });

    // smooth scroll to index
    window.pickerScrollTo = (el, index) => {
        if (!el) return;
        const top = index * itemHeight;
        el.scrollTo({ top: top, behavior: 'smooth' });
    };

    // initial call
    update();
};
