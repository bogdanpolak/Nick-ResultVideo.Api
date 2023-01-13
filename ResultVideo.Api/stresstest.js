import { check } from 'k6';
import http from 'k6/http';

export const options = {
    vus: 10,
    duration: '1m',
    insecureSkipTLSVerify: true
};

export default () => {
    const url = 'https://localhost:5001/customers';
    const payload = JSON.stringify({
        fullName: "Nick Chapsas",
        email: "",
        gitHubUsername: "nickchapsas",
        dateOfBirth: "1993-04-20"
    });
    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };
    
    
    const res = http.post(url, payload, params);

    check(res, {
        'is status 400': (r) => r.status === 400,
    });
};
