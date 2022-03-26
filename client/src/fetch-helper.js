/**
 * Combines all the required header details to be used in the fetch API
 * @param {object} customHeaders
 * @param {boolean} isStandardPayload
 * @returns Headers
 */
const createRequestHeaders = (customHeaders, isStandardPayload) => {
  let headers = {
    ...customHeaders,
  };

  if (isStandardPayload) {
    headers = {
      ...headers,
      "Content-Type": "application/json",
    };
  }

  return new Headers(headers);
};

/**
 * Converts the format of the result's text to a JSON object
 * @param {Promise.<Response>} result
 * @returns object
 */
const parseTextToJson = async (result) => {
  return result.text().then((text) => {
    return text ? JSON.parse(text) : null; //https://developer.mozilla.org/en-US/docs/Web/API/Response
  });
};

/**
 * Determines if the request body is a standard payload that can be stringified or a special case like a file upload request
 * @param {object} body
 * @returns boolean
 */
const isStandardPayload = (body) => {
  if (body instanceof FormData) {
    return false;
  }

  return true;
};

/**
 * Calls the actual fetch API
 * @param {string} subURL
 * @param {string} method
 * @param {object} customHeaders
 * @param {object} body
 * @param {object} customOptions
 * @returns object
 */
const baseFetch = async (
  subURL,
  method,
  customHeaders,
  customOptions,
  body
) => {
  const isStandardRequest = isStandardPayload(body);
  const headers = createRequestHeaders(customHeaders, isStandardRequest);

  const requestURL = `${subURL}`;
  const requestOptions = {
    ...customOptions,
    method: method,
    body: isStandardRequest ? JSON.stringify(body) : body,
    headers, //using a different name here returns a 401 error
  };

  const response = await fetch(requestURL, requestOptions);
  const jsonResponse = await parseTextToJson(response);

  if (response.status >= 200 && response.status < 300) {
    return jsonResponse;
  } else if (response.status >= 400 && response.status < 500) {
    // throw new Exception(jsonResponse, response.status);
  } else {
    // throw new ServerError(jsonResponse, response.status);
  }
};

/**
 * In charge with calling external GET APIs
 * @param {string} subURL
 * @param {object} customHeaders
 * @param {object} customOptions
 * @returns object
 */
export const get = async (
  subURL,
  customHeaders = undefined,
  customOptions = undefined
) => {
  return baseFetch(subURL, "GET", customHeaders, customOptions, undefined); // An async function always wraps the return value in a Promise. Using return await is therefore redundant.
};
