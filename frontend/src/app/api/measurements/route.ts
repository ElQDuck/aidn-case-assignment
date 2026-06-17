import { NextRequest, NextResponse } from 'next/server'

export async function POST(request: NextRequest) {
  try {
    const body = await request.json()
    const backendUrl = process.env.BACKEND_URL ?? 'http://localhost:5171'

    const response = await fetch(`${backendUrl}/NEWS`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body),
    })

    const data = await response.json()

    if (!response.ok) {
      // Out of range — tell the user what value is invalid
      if (data.title === 'invalid value' && data.detail) {
        return NextResponse.json({ error: data.detail }, { status: 400 })
      }

      // All other errors — generic message, not the user's fault
      return NextResponse.json(
        { error: 'Something went wrong on our end. Please try again.' },
        { status: 500 }
      )
    }

    return NextResponse.json(data, { status: 200 })
  } catch (e) {
    console.error('Route error:', e)
    return NextResponse.json({ error: 'Invalid request.' }, { status: 400 })
  }
}