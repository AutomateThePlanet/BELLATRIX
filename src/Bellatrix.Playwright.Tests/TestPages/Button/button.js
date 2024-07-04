var buttons = document.querySelectorAll('input');
for (var i = 0; i < buttons.length; i++) {
    var self = buttons[i];

    self.addEventListener('click', function (event) {
        if (this.value === 'Start') {
            this.value = 'Stop';
        } else {
            this.value = 'Start';
        }
    }, false);
}

var buttons1 = document.querySelectorAll('button');
for (var i = 0; i < buttons1.length; i++) {
    var self = buttons1[i];

    self.addEventListener('click', function (event) {
        if (this.innerHTML === 'Start') {
            this.innerHTML = 'Stop';
        } else {
            this.innerHTML = 'Start';
        }
    }, false);
}