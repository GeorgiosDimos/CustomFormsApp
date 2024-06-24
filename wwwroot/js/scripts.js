document.addEventListener('DOMContentLoaded', () => {
    const formList = document.getElementById('form-list');
    const createFormBtn = document.getElementById('create-form-btn');
    const formModal = document.getElementById('form-modal');
    const closeModal = document.getElementsByClassName('close')[0];
    const customForm = document.getElementById('custom-form');
    const fieldsContainer = document.getElementById('fields-container');
    const addFieldBtn = document.getElementById('add-field-btn');
    const formTitle = document.getElementById('form-title');
    const formDescription = document.getElementById('form-description');
    const formId = document.getElementById('form-id');
    const modalTitle = document.getElementById('modal-title');

    // Open modal
    createFormBtn.onclick = () => {
        formId.value = '';
        formTitle.value = '';
        formDescription.value = '';
        fieldsContainer.innerHTML = '';
        modalTitle.textContent = 'Create Form';
        formModal.style.display = 'block';
    };

    // Close modal
    closeModal.onclick = () => {
        formModal.style.display = 'none';
    };

    // Close modal when clicking outside
    window.onclick = (event) => {
        if (event.target == formModal) {
            formModal.style.display = 'none';
        }
    };

    // Add new field
    addFieldBtn.onclick = () => {
        if (fieldsContainer.children.length >= 10) {
            alert('Maximum 10 fields allowed.');
            return;
        }
        const fieldDiv = document.createElement('div');
        fieldDiv.className = 'field';
        fieldDiv.innerHTML = `
            <input type="text" placeholder="Field Name" required>
            <button type="button">Remove</button>
        `;
        fieldDiv.querySelector('button').onclick = () => fieldDiv.remove();
        fieldsContainer.appendChild(fieldDiv);
    };

    // Submit form
    customForm.onsubmit = async (event) => {
        event.preventDefault();
        const fields = Array.from(fieldsContainer.children).map(fieldDiv => ({
            name: fieldDiv.querySelector('input').value.trim(),
            value: ''
        }));

        const form = {
            title: formTitle.value.trim(),
            description: formDescription.value.trim(),
            fields: fields
        };

        if (formId.value) {
            await updateForm(formId.value, form);
        } else {
            await createForm(form);
        }

        formModal.style.display = 'none';
        loadForms();
    };

    // Load forms from the server
    async function loadForms() {
        try {
            const response = await fetch('/api/forms');
            if (!response.ok) {
                throw new Error('Failed to load forms');
            }
            const forms = await response.json();
            formList.innerHTML = forms.map(form => `
                <div class="form-item">
                    <h3>${form.title}</h3>
                    <p>${form.description}</p>
                    <p>Created: ${new Date(form.dateCreated).toLocaleString()}</p>
                    <p>Updated: ${new Date(form.dateUpdated).toLocaleString()}</p>
                    <button onclick="editForm(${form.id})">Edit</button>
                    <button onclick="deleteForm(${form.id})">Delete</button>
                </div>
            `).join('');
        } catch (error) {
            console.error('Error loading forms:', error.message);
        }
    }

    // Create form
    async function createForm(form) {
        try {
            const response = await fetch('/api/forms', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(form)
            });
            if (!response.ok) {
                throw new Error('Failed to create form');
            }
            loadForms();
        } catch (error) {
            console.error('Error creating form:', error.message);
        }
    }

    // Update form
    async function updateForm(id, form) {
        try {
            const response = await fetch(`/api/forms/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(form)
            });
            if (!response.ok) {
                throw new Error('Failed to update form');
            }
            loadForms();
        } catch (error) {
            console.error('Error updating form:', error.message);
        }
    }

    // Delete form
    window.deleteForm = async (id) => {
        try {
            const response = await fetch(`/api/forms/${id}`, {
                method: 'DELETE'
            });
            if (!response.ok) {
                throw new Error('Failed to delete form');
            }
            loadForms();
        } catch (error) {
            console.error('Error deleting form:', error.message);
        }
    }

    // Edit form
    window.editForm = async (id) => {
        try {
            const response = await fetch(`/api/forms/${id}`);
            if (!response.ok) {
                throw new Error('Failed to fetch form for editing');
            }
            const form = await response.json();

            formId.value = form.id;
            formTitle.value = form.title;
            formDescription.value = form.description;
            fieldsContainer.innerHTML = form.fields.map(field => `
                <div class="field">
                    <input type="text" value="${field.name}" required>
                    <button type="button">Remove</button>
                </div>
            `).join('');

            modalTitle.textContent = 'Edit Form';
            formModal.style.display = 'block';

            // Add event listeners for dynamically added remove buttons
            const removeFieldBtns = document.querySelectorAll('.field button');
            removeFieldBtns.forEach(btn => {
                btn.addEventListener('click', () => btn.parentElement.remove());
            });
        } catch (error) {
            console.error('Error editing form:', error.message);
        }
    };

    // Initial load of forms
    loadForms();
});
