function initializeEditor() {
    if (window.Quill && document.querySelector('.ql-editor')) {
        var quill = new Quill('#editor', {
            modules: {
                toolbar: '#toolbar'
            },
            theme: 'snow'
        });
    } else {
        setTimeout(initializeEditor, 100);
    }
}