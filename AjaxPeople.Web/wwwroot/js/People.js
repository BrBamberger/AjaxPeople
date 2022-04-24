$(() => {

    loadPeople();
    function loadPeople() {
        $.get('/home/getall', function (people) {
            $("#people-table tr:gt(0)").remove();
            people.forEach(person => {
                $("#people-table tbody").append(`
            <tr>
            <td>${person.firstName}</td>
            <td>${person.lastName}</td>
            <td>${person.age}</td>
            <td>
                <button class="btn btn-warning"
                data-id="${person.id}" data-first-name="${person.firstName}"
            data-last-name="${person.lastName}" data-age="${person.age}"
            id="edit-person">Edit</button>
            </td>
            <td>
                <button data-id="${person.id}" class="btn btn-danger" id="delete-person">Delete</button>
            </td>
        </tr>`);
            });
        });
    }

    $('#add-person').on('click', function () {
        console.log('inside the button');
        const firstName = $('#first-name').val();
        const lastName = $('#last-name').val();
        const age = $('#age').val();

        $.post('/home/addperson', { firstName, lastName, age }, function (person) {
            loadPeople();
            $("#first-name").val('');
            $("#last-name").val('');
            $("#age").val('');
        });
    });

    $('#people-table').on('click', "#delete-person", function () {
        console.log('inside delete button');
        const button = $(this);
        const id = button.data('id');

            $.post('/home/deleteperson', { id }, function() {
                loadPeople();
            });
    });


    $('#people-table').on('click', "#edit-person", function () {
        console.log('inside edit button');
        console.log(id);
        const button = $(this);
        const id = button.data('id');
        const firstName = button.data('first-name');
        const lastName = button.data('last-name');
        const age = button.data('age');
        $('.modal').data('id', id);

        $('#first-name').val(firstName);
        $('#last-name').val(lastName);
        $('#age').val(age);
        $('.modal').modal();

        $.post('/home/editperson', { firstName, lastName, age }, function () {
            loadPeople();
        });

        $('#update').on('click', function () {
            const id = $('.modal').data('id');
            const firstName = $('#first-name').val();
            const lastName = $('#last-name').val();
            const age = $('#age').val();
        })
    });
});