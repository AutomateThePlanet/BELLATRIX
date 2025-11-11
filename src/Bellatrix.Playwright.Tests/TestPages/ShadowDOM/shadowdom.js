// BASIC SHADOW DOM EXAMPLE
const basicShadowDOMHost = document.querySelector("#basicShadowHost");
const basicShadowRoot = basicShadowDOMHost.attachShadow({ mode: "open" });

// SELECT ELEMENT
const selectLabel = document.createElement("label");
selectLabel.setAttribute("for", "shadowSelect");
selectLabel.innerText = "Select element inside the shadow root.";
const select = document.createElement("select");
select.setAttribute("name", "select");
select.setAttribute("id", "shadowSelect");
const option1 = document.createElement("option");
option1.setAttribute("value", "bella1");
option1.innerText = "Bellatrix";
const option2 = document.createElement("option");
option2.setAttribute("value", "bella2");
option2.innerText = "Is";
const option3 = document.createElement("option");
option3.setAttribute("value", "bella3");
option3.innerText = "Awesome";
select.appendChild(option1);
select.appendChild(option2);
select.appendChild(option3);

basicShadowRoot.appendChild(selectLabel);
basicShadowRoot.appendChild(select);

// COMPLEX SHADOW DOM EXAMPLE
const complexShadowDOMHost = document.querySelector("#complexShadowHost");
const complexShadowRoot = complexShadowDOMHost.attachShadow({ mode: "open" });

// Function to create the table and append it to a shadow root
function createTableAndAppendToShadowRoot(shadowRoot) {
    // Create the table element
    const table = document.createElement('table');
    table.id = 'shadowTable';
    table.className = 'tablesorter';

    // Create the thead element
    const thead = document.createElement('thead');
    const theadRow = document.createElement('tr');
    const headers = ['Last Name', 'First Name', 'Email', 'Due', 'Web Site', 'Action'];

    headers.forEach(headerText => {
        const th = document.createElement('th');
        const span = document.createElement('span');
        span.textContent = headerText;
        th.appendChild(span);
        theadRow.appendChild(th);
    });

    thead.appendChild(theadRow);
    table.appendChild(thead);

    // Create the tbody element
    const tbody = document.createElement('tbody');
    const rows = [
        ['Smith', 'John', 'jsmith@gmail.com', '$50.00', 'http://www.jsmith.com'],
        ['Bach', 'Frank', 'fbach@yahoo.com', '$51.00', 'http://www.frank.com'],
        ['Doe', 'Jason', 'jdoe@hotmail.com', '$100.00', 'http://www.jdoe.com'],
        ['Conway', 'Tim', 'tconway@earthlink.net', '$50.00', 'http://www.timconway.com']
    ];

    rows.forEach(rowData => {
        const tr = document.createElement('tr');

        rowData.forEach(cellData => {
            const td = document.createElement('td');
            td.textContent = cellData;
            tr.appendChild(td);
        });

        const actionTd = document.createElement('td');
        const shadowHost = document.createElement("div");
        createActionAnchorsInsideShadowDOM(shadowHost);
        actionTd.appendChild(shadowHost);

        tr.appendChild(actionTd);

        tbody.appendChild(tr);
    });

    table.appendChild(tbody);

    shadowRoot.appendChild(table);
}

function createActionAnchorsInsideShadowDOM(element) {
    const shadow = element.attachShadow({ mode: "open" });

    const editLink = document.createElement('a');
    editLink.href = '#edit';
    editLink.textContent = 'edit';
    shadow.appendChild(editLink);

    const deleteLink = document.createElement('a');
    deleteLink.href = '#delete';
    deleteLink.textContent = 'delete';
    shadow.appendChild(deleteLink);
}

createTableAndAppendToShadowRoot(complexShadowRoot);


