'use client'

import { useState } from 'react'

interface FormValues {
  bodyTemperature: string
  heartrate: string
  respiratoryRate: string
}

interface FormErrors {
  bodyTemperature?: string
  heartrate?: string
  respiratoryRate?: string
}

const INITIAL_VALUES: FormValues = {
  bodyTemperature: '',
  heartrate: '',
  respiratoryRate: '',
}

export default function NewsForm() {
  const [values, setValues] = useState<FormValues>(INITIAL_VALUES)
  const [errors, setErrors] = useState<FormErrors>({})
  const [score, setScore] = useState<number | null>(null)
  const [isLoading, setIsLoading] = useState(false)
  const [apiError, setApiError] = useState<string | null>(null)

  function validate(vals: FormValues): FormErrors {
    const errs: FormErrors = {}
    if (vals.bodyTemperature === '') errs.bodyTemperature = 'Required'
    else if (isNaN(Number(vals.bodyTemperature))) errs.bodyTemperature = 'Must be a number'
    if (vals.heartrate === '') errs.heartrate = 'Required'
    else if (isNaN(Number(vals.heartrate))) errs.heartrate = 'Must be a number'
    if (vals.respiratoryRate === '') errs.respiratoryRate = 'Required'
    else if (isNaN(Number(vals.respiratoryRate))) errs.respiratoryRate = 'Must be a number'
    return errs
  }

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    const { name, value } = e.target
    setValues((prev) => ({ ...prev, [name]: value }))
    if (errors[name as keyof FormErrors]) {
      setErrors((prev) => ({ ...prev, [name]: undefined }))
    }
    setScore(null)
    setApiError(null)
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault()
    const validationErrors = validate(values)
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors)
      return
    }
    setIsLoading(true)
    setApiError(null)
    setScore(null)
    try {
        const response = await fetch('/api/measurements', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            measurements: [
            { type: 'TEMP', value: Number(values.bodyTemperature) },
            { type: 'HR',   value: Number(values.heartrate) },
            { type: 'RR',   value: Number(values.respiratoryRate) },
            ],
        }),
        })
      const data = await response.json()
      if (!response.ok) {
        setApiError(data.error ?? 'Something went wrong.')
        return
      }
      setScore(data.score)
    } catch {
      setApiError('Network error. Please try again.')
    } finally {
      setIsLoading(false)
    }
  }

  function handleReset() {
    setValues(INITIAL_VALUES)
    setErrors({})
    setScore(null)
    setApiError(null)
  }

  return (
    <div className="min-h-screen bg-gray-50 flex items-start justify-center pt-16 px-4">
      <div className="bg-white rounded-2xl shadow-sm border border-gray-100 w-full max-w-md p-8">
        <h1 className="text-xl font-semibold text-gray-900 mb-6">
          NEWS score calculator
        </h1>
        <form onSubmit={handleSubmit} noValidate>
          {/* Body temperature */}
          <div className="mb-5">
            <label className="block text-sm font-semibold text-gray-900 mb-0.5">Body temperature</label>
            <p className="text-sm text-gray-500 mb-1.5">Degrees celcius</p>
            <input
              type="number"
              name="bodyTemperature"
              value={values.bodyTemperature}
              onChange={handleChange}
              className={`w-full rounded-lg px-3 py-2.5 text-sm bg-[#F0EEFF] border ${
                errors.bodyTemperature ? 'border-red-400 focus:ring-red-300' : 'border-[#E5E0FF] focus:ring-purple-300'
              } focus:outline-none focus:ring-2 transition`}
            />
            {errors.bodyTemperature && <p className="text-xs text-red-500 mt-1">{errors.bodyTemperature}</p>}
          </div>

          {/* Heartrate */}
          <div className="mb-5">
            <label className="block text-sm font-semibold text-gray-900 mb-0.5">Heartrate</label>
            <p className="text-sm text-gray-500 mb-1.5">Beats per minute</p>
            <input
              type="number"
              name="heartrate"
              value={values.heartrate}
              onChange={handleChange}
              className={`w-full rounded-lg px-3 py-2.5 text-sm bg-[#F0EEFF] border ${
                errors.heartrate ? 'border-red-400 focus:ring-red-300' : 'border-[#E5E0FF] focus:ring-purple-300'
              } focus:outline-none focus:ring-2 transition`}
            />
            {errors.heartrate && <p className="text-xs text-red-500 mt-1">{errors.heartrate}</p>}
          </div>

          {/* Respiratory rate */}
          <div className="mb-7">
            <label className="block text-sm font-semibold text-gray-900 mb-0.5">Respiratory rate</label>
            <p className="text-sm text-gray-500 mb-1.5">Breaths per minute</p>
            <input
              type="number"
              name="respiratoryRate"
              value={values.respiratoryRate}
              onChange={handleChange}
              className={`w-full rounded-lg px-3 py-2.5 text-sm bg-[#F0EEFF] border ${
                errors.respiratoryRate ? 'border-red-400 focus:ring-red-300' : 'border-[#E5E0FF] focus:ring-purple-300'
              } focus:outline-none focus:ring-2 transition`}
            />
            {errors.respiratoryRate && <p className="text-xs text-red-500 mt-1">{errors.respiratoryRate}</p>}
          </div>

          {/* Actions */}
          <div className="flex items-center gap-4">
            <button
              type="submit"
              disabled={isLoading}
              className="bg-[#7C3AED] hover:bg-[#6D28D9] disabled:opacity-60 text-white text-sm font-medium px-5 py-2.5 rounded-full transition"
            >
              {isLoading ? 'Calculating…' : 'Calculate NEWS score'}
            </button>
            <button type="button" onClick={handleReset} className="text-sm text-gray-700 hover:text-gray-900 transition">
              Reset form
            </button>
          </div>

          {apiError && <p className="text-sm text-red-500 mt-4">{apiError}</p>}
        </form>

        {score !== null && (
          <div className="mt-6 rounded-xl border border-[#D4D0FF] bg-[#F5F3FF] px-4 py-3">
            <p className="text-sm text-gray-800">NEWS score: <strong>{score}</strong></p>
          </div>
        )}
      </div>
    </div>
  )
}