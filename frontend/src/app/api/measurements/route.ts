import { NextRequest, NextResponse } from 'next/server'

export async function POST(request: NextRequest) {
  try {
    const body = await request.json()

    const response = await fetch('http://localhost:5171/NEWS', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body),
    })

    const data = await response.json()

    if (!response.ok) {
      return NextResponse.json({ error: data.error ?? 'Backend error.' }, { status: response.status })
    }

    return NextResponse.json(data, { status: 200 })
  } catch (e) {
    console.error('Route error:', e)
    return NextResponse.json({ error: 'Invalid request.' }, { status: 400 })
  }
}