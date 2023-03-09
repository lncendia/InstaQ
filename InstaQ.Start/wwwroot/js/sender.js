class AsyncSender {
    constructor(messageBox) {
        this.messageBox = $(messageBox);
    }

    Add(selector, successMessage, callback) {
        let form = $(selector)
        let action = form.attr('action');
        form.data("validator").settings.submitHandler = async (form, ev) => {
            ev.preventDefault()
            await this.Send(action, new FormData(form), successMessage, callback)
        }
    }

    async Send(url, form, successMessage, callback) {
        let result = await fetch(url, {
            method: 'POST',
            body: form
        });
        if (result.ok) {
            this.Success(successMessage);
            if (callback !== undefined) callback(await result.text());
        } else this.BadRequest(await result.text())
    }

    Success(message) {
        this.messageBox.empty()
        this.messageBox.html('<div class="alert alert-success alert-dismissible fade show" role="alert">' + message + '<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>')
    }

    BadRequest(error) {
        this.messageBox.empty()
        this.messageBox.html('<div class="alert alert-danger alert-dismissible fade show" role="alert">' + error + '<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>')
    }
}