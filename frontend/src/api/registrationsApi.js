const API_BASE_URL = (
  import.meta.env.VITE_API_BASE_URL || "http://localhost:5205"
).replace(/\/+$/, "");

async function parseErrorMessage(response, fallbackMessage) {
  try {
    const data = await response.json();
    if (data?.errors && typeof data.errors === "object") {
      const messages = Object.values(data.errors).flat().filter(Boolean);
      if (messages.length > 0) {
        return messages.join(" ");
      }
    }
    if (data?.detail) return data.detail;
    if (data?.title) return data.title;
  } catch {
    // Ignore JSON parse failure and use fallback message
  }
  return fallbackMessage;
}

export async function getRegistrations({
  participantName = "",
  type = "",
  status = "",
  page = 1,
  pageSize = 10,
} = {}) {
  const params = new URLSearchParams();

  if (participantName && participantName.trim()) {
    params.set("participantName", participantName.trim());
  }
  if (type && type !== "All") {
    params.set("type", type);
  }
  if (status && status !== "All") {
    params.set("status", status);
  }
  params.set("page", String(page));
  params.set("pageSize", String(pageSize));

  const url = `${API_BASE_URL}/api/registrations?${params.toString()}`;

  let response;
  try {
    response = await fetch(url);
  } catch {
    throw new Error("Cannot reach the API.");
  }

  if (!response.ok) {
    const message = await parseErrorMessage(
      response,
      `Failed to load registrations (HTTP ${response.status}).`
    );
    throw new Error(message);
  }

  return response.json();
}

export async function getSummary() {
  const url = `${API_BASE_URL}/api/registrations/summary`;

  let response;
  try {
    response = await fetch(url);
  } catch {
    throw new Error("Cannot reach the API.");
  }

  if (!response.ok) {
    const message = await parseErrorMessage(
      response,
      `Failed to load summary (HTTP ${response.status}).`
    );
    throw new Error(message);
  }

  return response.json();
}